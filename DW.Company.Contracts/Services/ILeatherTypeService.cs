using DW.Company.Entities.Dto;
using DW.Company.Entities.Value;
using System.Collections.Generic;

namespace DW.Company.Contracts.Services
{
    public interface ILeatherTypeService
    {
        Response<IEnumerable<LeatherTypeDto>> GetAll();
        Response<Pagination<LeatherTypeDto>> Search(int page, int size, string field, string key);
        Response<LeatherTypeDto> Add(LeatherTypeDto value);
        Response<LeatherTypeDto> GetById(int id);
        Response Update(int id, LeatherTypeDto value);
        Response Delete(int id);
    }
}
