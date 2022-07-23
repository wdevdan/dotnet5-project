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
    public class ItemVersionService : IItemVersionService
    {
        private readonly IDBContext _db;
        private readonly IDBHelper _dbHelper;
        private readonly IMapper _mapper;
        private readonly IMasterSettings _settings;

        public ItemVersionService(IDBContext db, IMapper mapper, IMasterSettings settings, IDBHelper dbHelper)
        {
            _db = db;
            _mapper = mapper;
            _settings = settings;
            _dbHelper = dbHelper;
        }

        public Response<IEnumerable<ItemVersionDto>> GetAll()
        {
            var _response = new Response<IEnumerable<ItemVersionDto>>();
            var _ItemVersions = _db.ItemVersions.AsNoTracking();

            _response.Content = _ItemVersions.Select(s => _mapper.Map<ItemVersionDto>(s));
            return _response;
        }

        public Response<Pagination<ItemVersionDto>> Search(int page, int size, string field, string key)
        {
            var _query = _db.ItemVersions
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

            return new Response<Pagination<ItemVersionDto>>
            {
                Content = new Pagination<ItemVersionDto>
                {
                    Size = size,
                    Count = _count,
                    Items = _mapper.Map<ItemVersionDto[]>(_items),
                    PageCount = Convert.ToInt32(Math.Ceiling((decimal)_count / size)),
                    Page = page,
                }
            };
        }

        public Response<ItemVersionDto> GetById(int id)
        {
            var _response = new Response<ItemVersionDto>();
            var _Version = _db.ItemVersions
                .Where(
                    w => w.Id.Equals(id)
                )
                .AsNoTracking()
                .FirstOrDefault();

            _response.Content = _mapper.Map<ItemVersionDto>(_Version);
            return _response;
        }

        private void ValidateOnAdd(ItemVersionDto value)
        {
            if (_db.ItemVersions.Any(w => w.Name.Equals(value.Name))) throw new BadRequestException(ExceptionMessages.ERR0036);
        }

        public Response<ItemVersionDto> Add(ItemVersionDto value)
        {
            ValidateOnAdd(value);

            var _concrete = _mapper.Map<ItemVersion>(value);
            var _entry = _db.ItemVersions.Add(_concrete);

            _db.SaveChanges();
            return new Response<ItemVersionDto>
            {
                Content = _mapper.Map<ItemVersionDto>(_entry.Entity)
            };
        }

        public Response Delete(int id)
        {
            _dbHelper.Delete<ItemVersion>(w => w.Id == id);

            _db.SaveChanges();

            return new Response();
        }

        public Response Update(int id, ItemVersionDto value)
        {
            if (id != value.Id)
                throw new BadRequestException(ExceptionMessages.ERR0005);

            var _givenData = _mapper.Map<ItemVersion>(value);

            _dbHelper.Update<ItemVersion>(
                w => w.Id == id,
                (saved) => _givenData
            );

            _db.SaveChanges();
            return new Response();
        }
    }
}
