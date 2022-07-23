using AutoMapper;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;
using System.Linq;

namespace DW.Company.Api.Helpers
{
    public class ProductAutoMapperProfile: Profile
    {
        public ProductAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductFile, ProductFileDto>();
            CreateMap<ProductCategory, ProductCategoryDto>();

            CreateMap<ProductDto, Product>();
            CreateMap<ProductFileDto, ProductFile>();
            CreateMap<ProductCategoryDto, ProductCategory>();
        }
    }
}
