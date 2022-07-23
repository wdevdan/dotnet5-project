using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DW.Company.Contracts.Helpers
{
    public interface IDBHelper
    {
        TModel PrepareToAdd<TModel, TDto>(TDto dto);
        TModel PrepareToUpdate<TModel, TDto>(TModel model, TDto dto);
        TModel PrepareToUpdate<TModel, TDto>(Expression<Func<TModel, bool>> where, TDto dto) where TModel : class;
        TModel Add<TModel>(TModel value) where TModel : class;
        void Delete<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        TModel Update<TModel>(Expression<Func<TModel, bool>> where, Func<TModel, TModel> getModelToSave) where TModel : class;
        TModel Update<TModel, TDto>(Expression<Func<TModel, bool>> where, TDto dto) where TModel : class;
        TModel Update<TModel>(Expression<Func<TModel, bool>> where, Action<TModel> action) where TModel : class;
        void DeleteFromCollection<TModel>(Func<TModel, TModel, bool> any, Expression<Func<TModel, bool>> where, IEnumerable<TModel> givenData) where TModel : class;
        void DeleteFromCollection<TModel, TDto>(Func<TDto, TModel, bool> any, Expression<Func<TModel, bool>> where, IEnumerable<TDto> dtos) where TModel : class;
    }
}
