using AutoMapper;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;

namespace DW.Company.Api.Helpers
{
    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();
            CreateMap<UserChangeDto, User>();
        }
    }
}
