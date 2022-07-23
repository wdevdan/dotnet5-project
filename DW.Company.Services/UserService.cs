using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DW.Company.Common;
using DW.Company.Contracts.Data;
using DW.Company.Contracts.Helpers;
using DW.Company.Contracts.Services;
using DW.Company.Contracts.Settings;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;
using DW.Company.Entities.Exceptions;
using DW.Company.Entities.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DW.Company.Services
{
    public class UserService : IUserService
    {
        private readonly IDBContext _db;
        private readonly IDBHelper _dbHelper;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMasterSettings _settings;
        private readonly ISessionSettings _sessionSettings;

        public UserService(IDBContext db, IMapper mapper, IPasswordHasher passwordHasher, IMasterSettings settings, IDBHelper dbHelper, ISessionSettings sessionSettings)
        {
            _db = db;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _settings = settings;
            _dbHelper = dbHelper;
            _sessionSettings = sessionSettings;
        }

        public Response<IEnumerable<UserDto>> GetAll()
        {
            var _response = new Response<IEnumerable<UserDto>>();
            var _users = _db.UsersSession.AsNoTracking();

            _response.Content = _users.Select(s => _mapper.Map<UserDto>(s));
            return _response;
        }

        public Response<Pagination<UserDto>> Search(int page, int size, string field, string key)
        {
            var _query = _db.UsersSession
                .AsNoTracking();

            if (!string.IsNullOrEmpty(field) && !string.IsNullOrEmpty(key))
            {
                _query = _query
                    .Where(
                        $"{field}.ToLower().Contains(@0)", key.ToLower()
                ).OrderBy(field);

            }
            else
            {
                _query = _query.OrderBy(o => o.FirstName);
            }

            var _count = _query.Count();

            if (_count < size) page = 1;

            _query = _query
                .Skip((page - 1) * size)
                .Take(size);

            var _items = _query.ToList();

            return new Response<Pagination<UserDto>>
            {
                Content = new Pagination<UserDto>
                {
                    Size = size,
                    Count = _count,
                    Items = _mapper.Map<UserDto[]>(_items),
                    PageCount = Convert.ToInt32(Math.Ceiling((decimal)_count / size)),
                    Page = page,
                }
            };
        }

        public Response<UserDto> GetById(int id)
        {
            var _response = new Response<UserDto>();
            var _user = GetUserById(id);
            _response.Content = _mapper.Map<UserDto>(_user);
            return _response;
        }

        private void ValidateOnAdd(UserChangeDto value)
        {
            var _exists = _db.Users.Any(w => w.Login.Equals(value.Login));
            if (_exists)
                throw new BadRequestException(ExceptionMessages.ERR0009);
            if (value.Login.Equals(_settings.MASTERUSER))
                throw new BadRequestException(ExceptionMessages.ERR0011);
            if (value.ValidUntil == DateTime.MinValue || value.ValidSince == DateTime.MinValue)
                throw new BadRequestException(ExceptionMessages.ERR0034);
            if (string.IsNullOrEmpty(value.Password))
                throw new BadRequestException(ExceptionMessages.ERR0046);
        }

        private void OnSaveAdjustments(User value)
        {
            if (value.CustomerId != null) value.Role = Constants.CUSTOMERROLE;
        }

        public Response<UserDto> Add(UserChangeDto value)
        {
            ValidateOnAdd(value);

            value.Password = _passwordHasher.Hash(value.Password);

            var _concrete = _mapper.Map<User>(value);
            OnSaveAdjustments(_concrete);

            var _entry = _db.Users.Add(_concrete);
            _db.SaveChanges();
            return new Response<UserDto>
            {
                Content = _mapper.Map<UserDto>(_entry.Entity)
            };
        }

        private User GetMasterUser(LoginDto value)
        {
            if (value.Login.Equals(_settings.MASTERUSER))
                return GetMasterUser();
            return null;
        }

        private User GetMasterUser()
        {
            return new User
            {
                Id = 0,
                Email = _settings.MASTEREMAIL,
                FirstName = _settings.MASTERUSER,
                LastName = _settings.MASTERUSER,
                Login = _settings.MASTERUSER,
                Password = _settings.MASTERPASSWORD,
                Role = Constants.MASTERROLE
            };
        }

        public Response<UserDto> GetAuthenticationResponse(LoginDto value)
        {
            var _user = GetMasterUser(value);

            if (_user == null)
            {
                _user = _db.Users
                    .Where(
                        w => (
                            w.Login.Trim().ToLower().Equals(value.Login) ||
                            w.Email.Trim().ToLower().Equals(value.Login)
                        )
                    )
                    .AsNoTracking()
                    .FirstOrDefault();

                if (_user != null)
                    if (DateTime.Today.Date.AddDays(1) < _user.ValidSince || DateTime.Today.Date > _user.ValidUntil)
                        throw new BadRequestException(ExceptionMessages.ERR0012);
            }

            if (_user == null)
                throw new BadRequestException(ExceptionMessages.ERR0007);

            var _checkResult = _passwordHasher.Check(_user.Password, value.Password);

            if (!_checkResult.Verified)
                throw new BadRequestException(ExceptionMessages.ERR0008);

            return new Response<UserDto>
            {
                Content = (_mapper.Map<UserDto>(_user))
            };
        }

        public Response Delete(int id)
        {
            _dbHelper.Delete<User>(w => w.Id == id);
            _db.SaveChanges();

            return new Response();
        }

        public Response Update(int id, UserChangeDto value)
        {
            if (id != value.Id)
                throw new BadRequestException(ExceptionMessages.ERR0005);

            if (!string.IsNullOrEmpty(value.Password))
                value.Password = _passwordHasher.Hash(value.Password);

            var _givenData = _mapper.Map<User>(value);

            OnSaveAdjustments(_givenData);

            _dbHelper.Update<User>(
                w => w.Id == id,
                (saved) =>
                {
                    _givenData.Password = string.IsNullOrEmpty(value.Password) ? saved.Password : value.Password;
                    return _givenData;
                }
            );
            _db.SaveChanges();

            return new Response();
        }

        public Response ChangePassword(int id, ChangePasswordDto value)
        {
            if (string.IsNullOrEmpty(value?.Password))
                throw new BadRequestException(ExceptionMessages.ERR0014);
            var _data = _db.UsersSession
                .Where(w => w.Id == id)
                .FirstOrDefault();
            if (_data == null)
                throw new NotFoundException(ExceptionMessages.ERR0005);
            _data.Password = _passwordHasher.Hash(value.Password);
            _db.SaveChanges();

            return new Response();
        }

        public Response<UserDto> GetByToken()
        {
            User _user = null;
            var _master = _sessionSettings.Login.Equals(_settings.MASTERUSER);
            if (_master)
                _user = GetMasterUser();

            if (_user == null)
                _user = _db.UsersSession
                    .Where(w => w.Id == _sessionSettings.UserId)
                    .AsNoTracking()
                    .FirstOrDefault();

            if (_user == null) throw new NotFoundException(ExceptionMessages.ERR0007);
            if (_user.ValidUntil < DateTime.Today && !_master) throw new UnauthorizedException(ExceptionMessages.ERR0012);

            return new Response<UserDto>
            {
                Content = _mapper.Map<UserDto>(_user)
            };
        }

        public User GetUserById(int id)
        {
            return _db.UsersSession
                .Where(
                    w => w.Id.Equals(id)
                )
                .AsNoTracking()
                .FirstOrDefault();
        }
    }
}
