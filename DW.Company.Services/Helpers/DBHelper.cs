using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using DW.Company.Contracts.Data;
using DW.Company.Contracts.Helpers;
using DW.Company.Contracts.Settings;
using DW.Company.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DW.Company.Services.Helpers
{
    public class DBHelper : IDBHelper
    {
        private readonly IDBContext _db;
        private readonly ISessionSettings _sessionSettings;
        private readonly IMapper _mapper;
        public DBHelper(IDBContext db, ISessionSettings sessionSettings, IMapper mapper)
        {
            _db = db;
            _sessionSettings = sessionSettings;
            _mapper = mapper;
        }

        public TModel PrepareToAdd<TModel, TDto>(TDto dto)
        {
            var _converted = _mapper.Map<TDto, TModel>(dto);
            var _type = _converted.GetType();
            var _createdAtProp = _type.GetProperty("CreatedAt");
            var _updatedAtProp = _type.GetProperty("UpdatedAt");
            if (_createdAtProp != null)
                _createdAtProp.SetValue(_converted, DateTime.Now);
            if (_updatedAtProp != null)
                _updatedAtProp.SetValue(_converted, DateTime.Now);
            return _converted;
        }

        private TModel CommonPrepareToUpdate<TModel, TDto>(TModel saved, TDto dto)
        {
            var _converted = _mapper.Map<TDto, TModel>(dto);
            var _type = _converted.GetType();
            var _createdAtProp = _type.GetProperty("CreatedAt");
            var _updatedAtProp = _type.GetProperty("UpdatedAt");
            if (_createdAtProp != null)
                _createdAtProp.SetValue(_converted, _createdAtProp.GetValue(saved));
            if (_updatedAtProp != null)
                _updatedAtProp.SetValue(_converted, DateTime.Now);
            return _converted;
        }

        public TModel PrepareToUpdate<TModel, TDto>(TModel saved, TDto dto) => CommonPrepareToUpdate(saved, dto);

        public TModel PrepareToUpdate<TModel, TDto>(Expression<Func<TModel, bool>> where, TDto dto) where TModel : class
        {
            var _entry = _db.Set<TModel>();
            var _saved = _entry.Where(where).AsNoTracking().FirstOrDefault();
            if (_saved == null)
                return PrepareToAdd<TModel, TDto>(dto);
            return CommonPrepareToUpdate(_saved, dto);
        }

        public TModel Add<TModel>(TModel value) where TModel : class
        {
            var _entry = _db.Set<TModel>();
            var _entity = _entry.Add(value);
            return _entity.Entity;
        }

        public void Delete<TModel>(Expression<Func<TModel, bool>> where) where TModel : class
        {
            var _entry = _db.Set<TModel>();
            var _model = _entry.Where(where).AsNoTracking().FirstOrDefault();
            if (_model != null)
                _db.Entry(_model).State = EntityState.Deleted;
            else
                throw new NotFoundException(ExceptionMessages.ERR0013);
        }

        public void DeleteFromCollection<TModel>(Func<TModel, TModel, bool> any, Expression<Func<TModel, bool>> where, IEnumerable<TModel> givenData) where TModel : class
        {
            var _entry = _db.Set<TModel>();
            var _models = _entry.Where(where).AsNoTracking().ToList();
            foreach (var _model in _models)
                if (!givenData.Any(a => any(a, _model)))
                    _db.Entry(_model).State = EntityState.Deleted;
        }

        public TModel Update<TModel>(Expression<Func<TModel, bool>> where, Func<TModel, TModel> getModelToSave) where TModel : class
        {
            var _entry = _db.Set<TModel>();
            var _saved = _entry.Where(where).AsNoTracking().FirstOrDefault();
            if (_saved != null)
            {
                var _entity = _entry.Update(getModelToSave(_saved));
                return _entity.Entity;
            }
            throw new NotFoundException(ExceptionMessages.ERR0013);
        }


        private void ValidateToken<TModel, TDto>(TModel model, TDto dto)
        {
            var _typeModel = model.GetType();
            var _typeDto = dto.GetType();
            var _tokenModelProp = _typeModel.GetProperty("Token");
            var _tokenDtoProp = _typeDto.GetProperty("Token");
            if (_tokenModelProp != null)
            {
                var _modelValue = _tokenModelProp.GetValue(model)?.ToString();
                var _dtoValue = _tokenDtoProp?.GetValue(dto)?.ToString();

                if (!(_modelValue ?? string.Empty).Equals((_dtoValue ?? string.Empty)))
                    throw new BadRequestException(ExceptionMessages.ERR0017);
            }
        }

        public TModel Update<TModel, TDto>(Expression<Func<TModel, bool>> where, TDto dto) where TModel : class
        {
            var _entry = _db.Set<TModel>();
            var _saved = _entry.Where(where).AsNoTracking().FirstOrDefault();

            if (_saved != null)
            {
                ValidateToken(_saved, dto);
                var _prepared = PrepareToUpdate(_saved, dto);
                var _entity = _entry.Update(_prepared);
                return _entity.Entity;
            }
            else
                throw new NotFoundException(ExceptionMessages.ERR0013);
        }

        public void DeleteFromCollection<TModel, TDto>(Func<TDto, TModel, bool> any, Expression<Func<TModel, bool>> where, IEnumerable<TDto> dtos) where TModel : class
        {
            var _entry = _db.Set<TModel>();
            var _models = _entry.Where(where).AsNoTracking().ToList();
            foreach (var _model in _models)
                if (!dtos.Any(a => any(a, _model)))
                    _db.Entry(_model).State = EntityState.Deleted;
        }

        public TModel Update<TModel>(Expression<Func<TModel, bool>> where, Action<TModel> action) where TModel : class
        {
            var _entry = _db.Set<TModel>();
            var _saved = _entry.Where(where).AsNoTracking().FirstOrDefault();

            if (_saved != null)
            {
                action(_saved);
                var _entity = _entry.Update(_saved);
                return _entity.Entity;
            }
            else
                throw new NotFoundException(ExceptionMessages.ERR0013);
        }
    }
}
