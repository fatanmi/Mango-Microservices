﻿using AutoMapper;
using ProductAPI.Models;
using ProductAPI.Models.Dto;

namespace ProductAPI.Properties
{
    public class MappingConfig : Profile

    {
        public MappingConfig()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
