using DW.Company.Entities.Dto;
using DW.Company.Entities.Value;
using System.Collections.Generic;

namespace DW.Company.Contracts.Services
{
    public interface IDesignerService
    {
        Response<IEnumerable<DesignerDto>> GetAll();
        Response<Pagination<DesignerDto>> Search(int page, int size, string field, string key);
        Response<DesignerDto> Add(DesignerDto value);
        Response<DesignerDto> GetById(int id);
        Response Update(int id, DesignerDto value);
        Response Delete(int id);
    }
}
