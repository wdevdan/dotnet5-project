using DW.Company.Entities.Dto;
using DW.Company.Entities.Value;
using System.Collections.Generic;

namespace DW.Company.Contracts.Services
{
    public interface IItemVersionService
    {
        Response<IEnumerable<ItemVersionDto>> GetAll();
        Response<Pagination<ItemVersionDto>> Search(int page, int size, string field, string key);
        Response<ItemVersionDto> Add(ItemVersionDto value);
        Response<ItemVersionDto> GetById(int id);
        Response Update(int id, ItemVersionDto value);
        Response Delete(int id);
    }
}
