using DW.Company.Entities.Dto;
using DW.Company.Entities.Value;
using System.Collections.Generic;

namespace DW.Company.Contracts.Services
{
    public interface ICustomerProductService
    {
        Response<IEnumerable<CustomerProductDto>> GetAll();
        Response<Pagination<CustomerProductDto>> Search(int customerId, int page, int size, string field, string key);
        Response<Pagination<ProductDto>> SearchUnselectedProducts(int customerId, int page, int size, string field, string key);
        Response<CustomerProductDto> GetById(int id);
        Response<CustomerProductDto> Add(CustomerProductDto value);
        Response<CustomerProductDto> Update(int id, CustomerProductDto value);
        Response Delete(int id);
    }
}
