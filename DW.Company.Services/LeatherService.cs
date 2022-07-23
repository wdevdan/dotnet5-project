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
using DW.Company.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DW.Company.Services
{
    public class LeatherService : ILeatherService
    {
        private readonly IDBContext _db;
        private readonly IDBHelper _dbHelper;
        private readonly IMapper _mapper;
        private readonly IMasterSettings _settings;

        public LeatherService(IDBContext db, IMapper mapper, IMasterSettings settings, IDBHelper dbHelper)
        {
            _db = db;
            _mapper = mapper;
            _settings = settings;
            _dbHelper = dbHelper;
        }

        public Response<IEnumerable<LeatherDto>> GetAll()
        {
            var _leathers = _db.Leathers
                .AsNoTracking()
                .Includes()
                .ToList();

            return new Response<IEnumerable<LeatherDto>>
            {
                Content = _mapper.Map<LeatherDto[]>(_leathers)
            };
        }

        public Response<LeatherDto> GetById(int id)
        {
            var _leather = _db.Leathers
                .Where(
                    w => w.Id.Equals(id)
                )
                .Includes()
                .AsNoTracking()
                .FirstOrDefault();
            return new Response<LeatherDto>
            {
                Content = _mapper.Map<LeatherDto>(_leather)
            };
        }

        private void ValidateOnAdd(LeatherDto value)
        {
            if (_db.Leathers.Any(w => w.Name.Equals(value.Name))) throw new BadRequestException(ExceptionMessages.ERR0036);
        }

        public Response<LeatherDto> Add(LeatherDto value)
        {
            ValidateOnAdd(value);

            value.LeatherTypeId = value.LeatherTypeId ?? value.LeatherType?.Id;
            value.LeatherType = null;

            var _concrete = _mapper.Map<Leather>(value);
            var _entry = _db.Leathers.Add(_concrete);
            _db.SaveChanges();
            return new Response<LeatherDto>
            {
                Content = _mapper.Map<LeatherDto>(_entry.Entity)
            };
        }

        public Response<Pagination<LeatherDto>> Search(int page, int size, string field, string key)
        {
            var _query = _db.Leathers
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
                .Includes()
                .Skip((page - 1) * size)
                .Take(size);

            var _items = _query.ToList();

            return new Response<Pagination<LeatherDto>>
            {
                Content = new Pagination<LeatherDto>
                {
                    Size = size,
                    Count = _count,
                    Items = _mapper.Map<LeatherDto[]>(_items),
                    PageCount = Convert.ToInt32(Math.Ceiling((decimal)_count / size)),
                    Page = page,
                }
            };
        }

        public Response Delete(int id)
        {
            _dbHelper.Delete<Leather>(w => w.Id == id);

            _db.SaveChanges();

            return new Response();
        }

        public Response Update(int id, LeatherDto value)
        {
            if (id != value.Id)
                throw new BadRequestException(ExceptionMessages.ERR0005);

            var _givenData = _mapper.Map<Leather>(value);

            _dbHelper.Update<Leather>(
                w => w.Id == id,
                (saved) => _givenData
            );

            _db.SaveChanges();
            return new Response();
        }
    }
}
