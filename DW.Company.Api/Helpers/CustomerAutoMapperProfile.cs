using AutoMapper;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;

namespace DW.Company.Api.Helpers
{
    public class CustomerAutoMapperProfile: Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerProduct, CustomerProductDto>();
            
            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerProductDto, CustomerProduct>();
        }
    }
}
