using DW.Company.Entities.Dto;
using DW.Company.Entities.Value;
using System.Collections.Generic;

namespace DW.Company.Contracts.Services
{
    public interface IProductService
    {
        Response<ProductDto> GetById(int id);
        Response<IEnumerable<ProductDto>> GetAll();
        Response<Pagination<ProductDto>> Search(int page, int size, int? categoryId, int? designerId, string field, string key);
        Response<ProductDto> Add(ProductDto value);
        Response Update(int id, ProductDto value);
        Response Delete(int id);
    }
}
