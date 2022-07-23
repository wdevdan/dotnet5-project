using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class LeatherTypeService : ILeatherTypeService
    {
        private readonly IDBContext _db;
        private readonly IDBHelper _dbHelper;
        private readonly IMapper _mapper;
        private readonly IMasterSettings _settings;

        public LeatherTypeService(IDBContext db, IMapper mapper, IMasterSettings settings, IDBHelper dbHelper)
        {
            _db = db;
            _mapper = mapper;
            _settings = settings;
            _dbHelper = dbHelper;
        }

        public Response<IEnumerable<LeatherTypeDto>> GetAll()
        {
            var _response = new Response<IEnumerable<LeatherTypeDto>>();
            var _Leathers = _db.LeatherTypes.AsNoTracking();

            _response.Content = _Leathers.Select(s => _mapper.Map<LeatherTypeDto>(s));
            return _response;
        }

        public Response<LeatherTypeDto> GetById(int id)
        {
            var _leathers = _db.LeatherTypes
                .Where(
                    w => w.Id.Equals(id)
                )
                .AsNoTracking()
                .FirstOrDefault();

            return new Response<LeatherTypeDto>
            {
                Content = _mapper.Map<LeatherTypeDto>(_leathers)
            };
        }

        private void ValidateOnAdd(LeatherTypeDto value)
        {
            if (_db.LeatherTypes.Any(w => w.Name.Equals(value.Name))) throw new BadRequestException(ExceptionMessages.ERR0036);
        }

        public Response<LeatherTypeDto> Add(LeatherTypeDto value)
        {
            ValidateOnAdd(value);

            var _concrete = _mapper.Map<LeatherType>(value);
            var _entry = _db.LeatherTypes.Add(_concrete);

            _db.SaveChanges();
            return new Response<LeatherTypeDto>
            {
                Content = _mapper.Map<LeatherTypeDto>(_entry.Entity)
            };
        }

        public Response<Pagination<LeatherTypeDto>> Search(int page, int size, string field, string key)
        {
            var _query = _db.LeatherTypes
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
                _query = _query.OrderBy(o => o.Code);
            }

            var _count = _query.Count();

            if (_count < size) page = 1;

            _query = _query
                .Skip((page - 1) * size)
                .Take(size);

            var _items = _query.ToList();

            return new Response<Pagination<LeatherTypeDto>>
            {
                Content = new Pagination<LeatherTypeDto>
                {
                    Size = size,
                    Count = _count,
                    Items = _mapper.Map<LeatherTypeDto[]>(_items),
                    PageCount = Convert.ToInt32(Math.Ceiling((decimal)_count / size)),
                    Page = page,
                }
            };
        }

        public Response Delete(int id)
        {
            _dbHelper.Delete<LeatherType>(w => w.Id == id);

            _db.SaveChanges();

            return new Response();
        }

        public Response Update(int id, LeatherTypeDto value)
        {
            if (id != value.Id)
                throw new BadRequestException(ExceptionMessages.ERR0005);

            var _givenData = _mapper.Map<LeatherType>(value);

            _dbHelper.Update<LeatherType>(
                w => w.Id == id,
                (saved) => _givenData
            );

            _db.SaveChanges();
            return new Response();
        }
    }
}
