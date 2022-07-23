﻿using AutoMapper;
using DW.Company.Entities.Dto;
using DW.Company.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DW.Company.Api.Helpers
{
    public class FileAutoMapperProfile: Profile
    {
        public FileAutoMapperProfile()
        {
            CreateMap<FileItem, FileItemDto>();
            CreateMap<FileItemDto, FileItem>();
        }
    }
}
