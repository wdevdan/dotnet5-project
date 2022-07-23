using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DW.Company.Contracts.Data;
using DW.Company.Contracts.Helpers;
using DW.Company.Contracts.Services;
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
    public class CustomerProductService : ICustomerProductService
    {
        private readonly IDBContext _db;
        private readonly IDBHelper _dbHelper;
        private readonly IMapper _mapper;

        public CustomerProductService(IDBContext db, IDBHelper dbHelper, IMapper mapper)
        {
            _db = db;
            _dbHelper = dbHelper;
            _mapper = mapper;
        }

        public Response<CustomerProductDto> GetById(int id)
        {
            var _value = _db.CustomerProducts
                .Where(w => w.Id == id)
                .AsNoTracking()
                .Includes()
                .FirstOrDefault();

            if (_value == null) throw new NotFoundException(ExceptionMessages.ERR0013);

            return new Response<CustomerProductDto>
            {
                Content = _mapper.Map<CustomerProductDto>(_value)
            };
        }

        public Response<IEnumerable<CustomerProductDto>> GetAll()
        {
            var _items = _db.CustomerProducts
               .AsNoTracking()
               .Includes()
               .ToList();

            return new Response<IEnumerable<CustomerProductDto>>
            {
                Content = _mapper.Map<CustomerProductDto[]>(_items)
            };
        }

        private void ValidateOnAdd(CustomerProductDto value)
        {
            var _exists = _db.CustomerProducts
                .Any(w => w.CustomerId == value.CustomerId && w.ProductId == value.ProductId);
            if (_exists)
                throw new BadRequestException(ExceptionMessages.ERR0049);
        }

        public Response<CustomerProductDto> Add(CustomerProductDto value)
        {
            ValidateOnAdd(value);
            var _concrete = _mapper.Map<CustomerProduct>(value);
            var _entity = _dbHelper.Add(_concrete);
            _db.SaveChanges();
            return new Response<CustomerProductDto>
            {
                Content = _mapper.Map<CustomerProductDto>(_entity)
            };
        }

        private void ValidateOnUpdate(CustomerProductDto value)
        {
            var _exists = _db.CustomerProducts
                .Any(w => w.CustomerId == value.Id && w.ProductId == value.ProductId && w.Id != value.Id);
            if (_exists)
                throw new BadRequestException(ExceptionMessages.ERR0049);
        }

        public Response<CustomerProductDto> Update(int id, CustomerProductDto value)
        {
            if (id != value.Id)
                throw new BadRequestException(ExceptionMessages.ERR0005);

            ValidateOnUpdate(value);

            var _entity = _dbHelper.Update<CustomerProduct>(w => w.Id == id, (_) => _mapper.Map<CustomerProduct>(value));

            _db.SaveChanges();

            return new Response<CustomerProductDto> { Content = _mapper.Map<CustomerProductDto>(_entity) };
        }

        public Response Delete(int id)
        {
            _dbHelper.Delete<CustomerProduct>(w => w.Id == id);

            _db.SaveChanges();
            return new Response();
        }

        public Response<Pagination<CustomerProductDto>> Search(int customerId, int page, int size, string field, string key)
        {
            var _query = _db.CustomerProducts
                .Where(w => w.CustomerId == customerId)
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
                _query = _query.OrderBy(o => o.Product.Code);
            }

            var _count = _query.Count();

            if (_count < size) page = 1;

            _query = _query
                .Includes()
                .Skip((page - 1) * size)
                .Take(size);

            var _items = _query.ToList();

            return new Response<Pagination<CustomerProductDto>>
            {
                Content = new Pagination<CustomerProductDto>
                {
                    Size = size,
                    Count = _count,
                    Items = _mapper.Map<CustomerProductDto[]>(_items),
                    PageCount = Convert.ToInt32(Math.Ceiling((decimal)_count / size)),
                    Page = page,
                }
            };
        }

        public Response<Pagination<ProductDto>> SearchUnselectedProducts(int customerId, int page, int size, string field, string key)
        {
            var _query = _db.Products
                .Where(
                    w => !_db.CustomerProducts.Any(
                        a => a.CustomerId == customerId &&
                        a.ProductId == w.Id
                    )
                )
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

            return new Response<Pagination<ProductDto>>
            {
                Content = new Pagination<ProductDto>
                {
                    Size = size,
                    Count = _count,
                    Items = _mapper.Map<ProductDto[]>(_items),
                    PageCount = Convert.ToInt32(Math.Ceiling((decimal)_count / size)),
                    Page = page,
                }
            };
        }
    }
}
