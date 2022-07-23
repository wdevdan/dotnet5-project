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
    public class DesignerService : IDesignerService
    {
        private readonly IDBContext _db;
        private readonly IDBHelper _dbHelper;
        private readonly IMapper _mapper;
        private readonly IMasterSettings _settings;

        public DesignerService(IDBContext db, IMapper mapper, IMasterSettings settings, IDBHelper dbHelper)
        {
            _db = db;
            _mapper = mapper;
            _settings = settings;
            _dbHelper = dbHelper;
        }

        public Response<IEnumerable<DesignerDto>> GetAll()
        {
            var _designers = _db.Designers
                .AsNoTracking()
                .Includes();

            return new Response<IEnumerable<DesignerDto>>
            {
                Content = _mapper.Map<DesignerDto[]>(_designers)
            };
        }

        public Response<Pagination<DesignerDto>> Search(int page, int size, string field, string key)
        {
            var _query = _db.Designers
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
                _query = _query.OrderBy(o => o.Id);
            }

            var _count = _query.Count();

            if (_count < size) page = 1;

            _query = _query
                .Includes()
                .Skip((page - 1) * size)
                .Take(size);

            var _items = _query.ToList();

            return new Response<Pagination<DesignerDto>>
            {
                Content = new Pagination<DesignerDto>
                {
                    Size = size,
                    Count = _count,
                    Items = _mapper.Map<DesignerDto[]>(_items),
                    PageCount = Convert.ToInt32(Math.Ceiling((decimal)_count / size)),
                    Page = page,
                }
            };
        }

        public Response<DesignerDto> GetById(int id)
        {
            var _designer = _db.Designers
                .Where(
                    w => w.Id == id
                )
                .AsNoTracking()
                .Includes()
                .FirstOrDefault();

            return new Response<DesignerDto>
            {
                Content = _mapper.Map<DesignerDto>(_designer)
            };
        }

        private void ValidateOnAdd(DesignerDto value)
        {
            if (_db.Designers.Any(w => w.Name.Equals(value.Name))) throw new BadRequestException(ExceptionMessages.ERR0036);
        }

        public Response<DesignerDto> Add(DesignerDto value)
        {
            ValidateOnAdd(value);

            value.FileItemId = value.FileItemId ?? value.FileItem?.Id;
            value.FileItem = null;

            var _prepared = _dbHelper.PrepareToAdd<Designer, DesignerDto>(value);
            var _saved = _dbHelper.Add(_prepared);

            _db.SaveChanges();
            return new Response<DesignerDto>
            {
                Content = _mapper.Map<DesignerDto>(_saved)
            };
        }

        public Response Delete(int id)
        {
            _dbHelper.Delete<Designer>(w => w.Id == id);

            _db.SaveChanges();

            return new Response();
        }

        public Response Update(int id, DesignerDto value)
        {
            if (id != value.Id)
                throw new BadRequestException(ExceptionMessages.ERR0005);

            var _givenData = _mapper.Map<Designer>(value);

            _dbHelper.Update<Designer>(
                w => w.Id == id,
                (saved) => _givenData
            );

            _db.SaveChanges();
            return new Response();
        }
    }
}
