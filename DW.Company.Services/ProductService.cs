using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DW.Company.Common;
using DW.Company.Contracts.Data;
using DW.Company.Contracts.Helpers;
using DW.Company.Contracts.Services;
using DW.Company.Contracts.Settings;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;
using DW.Company.Entities.Enums;
using DW.Company.Entities.Exceptions;
using DW.Company.Entities.Value;
using DW.Company.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace DW.Company.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IDBHelper _dbHelper;
        private readonly IDBContext _db;
        private readonly ISessionSettings _sessionSettings;
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;

        public ProductService(
            IDBContext db, 
            IMapper mapper, 
            IDBHelper dbHelper, 
            ISessionSettings sessionSettings, 
            IUserService userService,
            ICustomerService customerService
        )
        {
            _mapper = mapper;
            _db = db;
            _dbHelper = dbHelper;
            _sessionSettings = sessionSettings;
            _userService = userService;
            _customerService = customerService;
            
        }

        private ProductDto MapOneToDto(Product value)
        {
            return _mapper.Map<ProductDto>(value)
                    .ForMember(
                            (m) => m.Files, 
                            (s, a) => s.Files = a.OrderBy(o => o.Order).ToList()
                    );
        }

        private IEnumerable<ProductDto> MapManyToDto(IEnumerable<Product> value)
        {
            return _mapper.Map<IEnumerable<ProductDto>>(value)
                    .ForMember(
                            (m) => m.Files, 
                            (s, a) => s.Files = a.OrderBy(o => o.Order).ToList()
                    );
        }

        private void ValidateOnAdd(ProductDto value)
        {
            var _exists = _db.Products.Any(w => w.Name.Equals(value.Name));
            if (_exists) throw new BadRequestException(ExceptionMessages.ERR0037);

            var _existsTwo = _db.Products.Any(w => w.Code.Equals(value.Code));
            if (_existsTwo) throw new BadRequestException(ExceptionMessages.ERR0038);
        }

        public Response<ProductDto> Add(ProductDto value)
        {
            ValidateOnAdd(value);

            var _concrete = _mapper.Map<Product>(value);
            var _entity = _dbHelper.Add(_concrete);

            _db.SaveChanges();
            return new Response<ProductDto>
            {
                Content = MapOneToDto(_entity)
            };
        }

        public Response<ProductDto> GetById(int id)
        {
            var _product = _db.Products
                .Where(w => w.Id == id)
                .AsNoTracking()
                .Includes()
                .FirstOrDefault();

            return new Response<ProductDto>
            {
                Content = MapOneToDto(_product)
            };
        }

        private IQueryable<Product> GetAnonymousQuery(IQueryable<Product> baseQuery)
        {
            return baseQuery
                .Where(
                    w =>
                        !_db.CustomerProducts.Any(
                            a => a.ProductId == w.Id
                        )
                );
        }

        private IQueryable<Product> GetCustomerQuery(IQueryable<Product> baseQuery)
        {
            if (_sessionSettings.UserId == null) throw new UnauthorizedException(ExceptionMessages.ERR0016);
            
            var _user = _userService.GetUserById((int)_sessionSettings.UserId);
            if (_user == null) throw new NotFoundException(ExceptionMessages.ERR0048);
            
            if (_user.CustomerId == null) throw new BadRequestException(ExceptionMessages.ERR0047);

            var _customer = _customerService.GetCustomerById((int)_user.CustomerId);
            if (_customer == null) throw new NotFoundException(ExceptionMessages.ERR0045);

            if (_customer.ProductLinkType == (int)ProductLinkType.NONE)
            {
                return baseQuery
                    .Where(
                        w => _db.CustomerProducts
                            .Any(
                                a => a.CustomerId != _customer.Id &&
                                a.ProductId == w.Id &&
                                a.Customer.ProductLinkType == (int)ProductLinkType.ALLOWED
                        )
                    );
            } else if (_customer.ProductLinkType == (int)ProductLinkType.ALLOWED)
            {
                return baseQuery
                    .Where(
                        w => (
                            _db.CustomerProducts
                                .Any(
                                    a => a.CustomerId != _customer.Id &&
                                    a.ProductId == w.Id &&
                                    a.Customer.ProductLinkType == (int)ProductLinkType.ALLOWED
                                ) && 
                                !_db.CustomerProducts
                                    .Any(
                                        a => a.CustomerId == _customer.Id && 
                                        a.ProductId == w.Id
                                    )
                            ) || 
                            _db.CustomerProducts
                                .Any(
                                    a => a.CustomerId == _customer.Id &&
                                    a.ProductId == w.Id
                                )
                    );
            }
            if (_customer.ProductLinkType == (int)ProductLinkType.BLOCKED)
            {
                return baseQuery
                    .Where(
                        w => 
                            _db.CustomerProducts
                                .Any(
                                    a => a.CustomerId != _customer.Id &&
                                    a.ProductId == w.Id &&
                                    a.Customer.ProductLinkType == (int)ProductLinkType.ALLOWED
                                ) &&
                            !_db.CustomerProducts
                                .Any(
                                    a => a.CustomerId == _customer.Id &&
                                    a.ProductId == w.Id
                                )
                    );
            }
            throw new BadRequestException(ExceptionMessages.ERR0044);
        }

        private IQueryable<Product> GetDefaultQuery()
        {
            var _query = _db.Products.AsQueryable();
            if (_sessionSettings.UserId == null) return GetAnonymousQuery(_query);
            if (_sessionSettings.Role.Equals(Constants.CUSTOMERROLE)) return GetCustomerQuery(_query);
            return _query;
        }

        public Response<Pagination<ProductDto>> Search(int page, int size, int? categoryId, int? designerId, string field, string key)
        {
            var _query = GetDefaultQuery().AsNoTracking();

            if ((categoryId ?? 0) > 0)
            {
                _query = _query
                    .Where(w => w.Categories.Any(a => a.CategoryId == categoryId));
            }

            if ((designerId ?? 0) > 0)
            {
                _query = _query
                    .Where(w => w.DesignerId == designerId);
            }

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
                    Items = MapManyToDto(_items),
                    PageCount = Convert.ToInt32(Math.Ceiling((decimal)_count / size)),
                    Page = page,
                }
            };
        }

        public Response<IEnumerable<ProductDto>> GetAll()
        {
            var _response = new Response<IEnumerable<ProductDto>>();
            var _products = GetDefaultQuery()
                .AsNoTracking()
                .Includes()
                .ToList();
            _response.Content = MapManyToDto(_products);
            return _response;
        }

        public Response Delete(int id)
        {
            _dbHelper.Delete<Product>(w => w.Id == id);

            _db.SaveChanges();
            return new Response();
        }

        public Response Update(int id, ProductDto value)
        {
            if (id != value.Id)
                throw new BadRequestException(ExceptionMessages.ERR0005);

            foreach (var _file in value.Files) 
                if (value.Files.Any(a => a.Order == _file.Order && a.FileItemId != _file.FileItemId)) 
                    _file.Order = value.Files.Count();

            var _givenData = _mapper.Map<Product>(value);

            _dbHelper.DeleteFromCollection(
                (a, b) => a.Id == b.Id,
                w => w.ProductId == id,
                _givenData.Files
            );

            _dbHelper.DeleteFromCollection(
                (a, b) => a.Id == b.Id,
                w => w.ProductId == id,
                _givenData.Categories
            );
            
            _dbHelper.Update<Product>(w => w.Id == id, (_) => _givenData);

            _db.SaveChanges();
            return new Response();
        }
    }
}
