using DW.Company.Entities.Dto;
using DW.Company.Entities.Value;
using System.Collections.Generic;

namespace DW.Company.Contracts.Services
{
    public interface ILeatherService
    {
        Response<IEnumerable<LeatherDto>> GetAll();
        Response<Pagination<LeatherDto>> Search(int page, int size, string field, string key);
        Response<LeatherDto> Add(LeatherDto value);
        Response<LeatherDto> GetById(int id);
        Response Update(int id, LeatherDto value);
        Response Delete(int id);
    }
}
