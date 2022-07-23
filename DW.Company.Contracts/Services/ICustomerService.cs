using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;
using DW.Company.Entities.Value;
using System.Collections.Generic;

namespace DW.Company.Contracts.Services
{
    public interface ICustomerService
    {
        Response<IEnumerable<CustomerDto>> GetAll();
        Response<Pagination<CustomerDto>> Search(int page, int size, string field, string key);
        Response<CustomerDto> Add(CustomerDto value);
        Response<CustomerDto> GetById(int id);
        Customer GetCustomerById(int id);
        Response Update(int id, CustomerDto value);
        Response UpdateProductLinkType(int id, int productLinkType);
        Response Delete(int id);
    }
}
