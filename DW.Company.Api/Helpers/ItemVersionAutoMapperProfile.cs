using AutoMapper;
using System.Linq;

using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;

namespace DW.Company.Api.Helpers
{
    public class ItemVersionAutoMapperProfile : Profile
    {
        public ItemVersionAutoMapperProfile()
        {
            CreateMap<ItemVersion, ItemVersionDto>();
            CreateMap<ItemVersionDto, ItemVersion>();
        }
    }
}
