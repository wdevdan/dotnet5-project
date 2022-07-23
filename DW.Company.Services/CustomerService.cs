using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DW.Company.Contracts.Data;
using DW.Company.Contracts.Helpers;
using DW.Company.Contracts.Services;
using DW.Company.Contracts.Settings;
using DW.Company.Entities.Entity;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Exceptions;
using DW.Company.Entities.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using DW.Company.Entities.Enums;

namespace DW.Company.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDBContext _db;
        private readonly IDBHelper _dbHelper;
        private readonly IMapper _mapper;
        private readonly IMasterSettings _settings;

        public CustomerService(IDBContext db, IMapper mapper, IMasterSettings settings, IDBHelper dbHelper)
        {
            _db = db;
            _mapper = mapper;
            _settings = settings;
            _dbHelper = dbHelper;
        }

        public Response<IEnumerable<CustomerDto>> GetAll()
        {
            var _response = new Response<IEnumerable<CustomerDto>>();
            var _Customers = _db.Customers.AsNoTracking();

            _response.Content = _Customers.Select(s => _mapper.Map<CustomerDto>(s));
            return _response;
        }

        public Response<CustomerDto> GetById(int id)
        {
            var _response = new Response<CustomerDto>();
            var _customer = GetCustomerById(id);

            _response.Content = _mapper.Map<CustomerDto>(_customer);
            return _response;
        }

        public Response<Pagination<CustomerDto>> Search(int page, int size, string field, string key)
        {
            var _query = _db.CustomersSession
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

            return new Response<Pagination<CustomerDto>>
            {
                Content = new Pagination<CustomerDto>
                {
                    Size = size,
                    Count = _count,
                    Items = _mapper.Map<CustomerDto[]>(_items),
                    PageCount = Convert.ToInt32(Math.Ceiling((decimal)_count / size)),
                    Page = page,
                }
            };
        }

        private void ValidateOnAdd(CustomerDto value)
        {
            if (_db.Customers.Any(w => w.Name.Equals(value.Name))) throw new BadRequestException(ExceptionMessages.ERR0036);
        }

        public Response<CustomerDto> Add(CustomerDto value)
        {
            ValidateOnAdd(value);

            var _concrete = _mapper.Map<Customer>(value);
            var _entry = _db.Customers.Add(_concrete);

            _db.SaveChanges();
            return new Response<CustomerDto>
            {
                Content = _mapper.Map<CustomerDto>(_entry.Entity)
            };
        }

        public Response Delete(int id)
        {
            _dbHelper.Delete<Customer>(w => w.Id == id);

            _db.SaveChanges();

            return new Response();
        }

        public Response Update(int id, CustomerDto value)
        {
            if (id != value.Id)
                throw new BadRequestException(ExceptionMessages.ERR0005);

            var _givenData = _mapper.Map<Customer>(value);

            _dbHelper.Update<Customer>(
                w => w.Id == id,
                (saved) => _givenData
            );

            _db.SaveChanges();
            return new Response();
        }

        public Customer GetCustomerById(int id)
        {
            return _db.CustomersSession
                .Where(
                    w => w.Id.Equals(id)
                )
                .AsNoTracking()
                .FirstOrDefault();
        }

        public Response UpdateProductLinkType(int id, int productLinkType)
        {
            _dbHelper.Update<Customer>(
                w => w.Id == id,
                (a) => a.ProductLinkType = productLinkType
            );
            _db.SaveChanges();
            return new Response();
        }
    }
}
