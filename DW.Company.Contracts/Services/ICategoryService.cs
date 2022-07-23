using DW.Company.Entities.Dto;
using DW.Company.Entities.Value;
using System.Collections.Generic;

namespace DW.Company.Contracts.Services
{
    public interface ICategoryService
    {
        Response<IEnumerable<CategoryDto>> GetAll();
        Response<Pagination<CategoryDto>> Search(int page, int size, string field, string key);
        Response<CategoryDto> Add(CategoryDto value);
        Response<CategoryDto> GetById(int id);
        Response Update(int id, CategoryDto value);
        Response Delete(int id);
    }
}
