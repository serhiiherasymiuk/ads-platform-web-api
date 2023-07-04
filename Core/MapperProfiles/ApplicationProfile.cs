﻿using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.MapperProfiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Category, GetCategoryDTO>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

            CreateMap<CreateCategoryDTO, Category>()
                            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? src.Image.GetHashCode() + "_" + src.Image.FileName : null));
        }
    }
}