using AutoMapper;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DW.Company.Api.Helpers
{
    public class LeatherAutoMapperProfile: Profile
    {
        public LeatherAutoMapperProfile ()
        {
            CreateMap<Leather, LeatherDto>();
            CreateMap<LeatherType, LeatherTypeDto>();

            CreateMap<LeatherTypeDto, LeatherType>();
            CreateMap<LeatherDto, Leather>();
        }
    }
}
