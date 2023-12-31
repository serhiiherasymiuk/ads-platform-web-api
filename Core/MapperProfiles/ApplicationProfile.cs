﻿using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Core.MapperProfiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<User, GetUserDTO>();

            CreateMap<EditUserDTO, User>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture != null ? Path.GetRandomFileName() : null));

            CreateMap<EditCategoryDTO, Category>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? Path.GetRandomFileName() : null));

            CreateMap<Category, GetCategoryDTO>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

            CreateMap<CreateCategoryDTO, Category>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? Path.GetRandomFileName() : null));

            CreateMap<CreateAdvertisementDTO, Advertisement>()
                .ForMember(dest => dest.AdvertisementImages, opt => opt.Ignore());

            CreateMap<Advertisement, GetAdvertisementDTO>();

            CreateMap<AdvertisementImage, GetAdvertisementImageDTO>().ReverseMap();
        }
    }
}