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
    public class CategoryService : ICategoryService
    {
        private readonly IDBContext _db;
        private readonly IDBHelper _dbHelper;
        private readonly IMapper _mapper;
        private readonly IMasterSettings _settings;

        public CategoryService(IDBContext db, IMapper mapper, IMasterSettings settings, IDBHelper dbHelper)
        {
            _db = db;
            _mapper = mapper;
            _settings = settings;
            _dbHelper = dbHelper;
        }

        public Response<IEnumerable<CategoryDto>> GetAll()
        {
            var _response = new Response<IEnumerable<CategoryDto>>();
            var _Categories = _db.Categories.AsNoTracking();

            _response.Content = _Categories.Select(s => _mapper.Map<CategoryDto>(s));
            return _response;
        }

        public Response<Pagination<CategoryDto>> Search(int page, int size, string field, string key)
        {
            var _query = _db.Categories
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

            return new Response<Pagination<CategoryDto>>
            {
                Content = new Pagination<CategoryDto>
                {
                    Size = size,
                    Count = _count,
                    Items = _mapper.Map<CategoryDto[]>(_items),
                    PageCount = Convert.ToInt32(Math.Ceiling((decimal)_count / size)),
                    Page = page,
                }
            };
        }

        public Response<CategoryDto> GetById(int id)
        {
            var _response = new Response<CategoryDto>();
            var _category = _db.Categories
                .Where(
                    w => w.Id.Equals(id)
                )
                .AsNoTracking()
                .FirstOrDefault();

            if (_category == null) throw new NotFoundException(ExceptionMessages.ERR0013);

            _response.Content = _mapper.Map<CategoryDto>(_category);
            return _response;
        }

        private void ValidateOnAdd(CategoryDto value)
        {
            if (_db.Categories.Any(w => w.Name.Equals(value.Name))) throw new BadRequestException(ExceptionMessages.ERR0043);
        }

        public Response<CategoryDto> Add(CategoryDto value)
        {
            ValidateOnAdd(value);

            var _concrete = _mapper.Map<Category>(value);
            var _entry = _db.Categories.Add(_concrete);

            _db.SaveChanges();
            return new Response<CategoryDto>
            {
                Content = _mapper.Map<CategoryDto>(_entry.Entity)
            };
        }

        public Response Delete(int id)
        {
            _dbHelper.Delete<Category>(w => w.Id == id);

            _db.SaveChanges();

            return new Response();
        }

        public Response Update(int id, CategoryDto value)
        {
            if (id != value.Id)
                throw new BadRequestException(ExceptionMessages.ERR0005);

            var _givenData = _mapper.Map<Category>(value);

            _dbHelper.Update<Category>(
                w => w.Id == id,
                (saved) => _givenData
            );

            _db.SaveChanges();
            return new Response();
        }
    }
}
