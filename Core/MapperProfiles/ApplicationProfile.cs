using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.MapperProfiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<User, GetUserDTO>();

            CreateMap<EditUserDTO, User>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture != null ? src.ProfilePicture.GetHashCode() + "_" + src.ProfilePicture.FileName : null));

            CreateMap<Subcategory, SubcategoryDTO>().ReverseMap();

            CreateMap<Category, GetCategoryDTO>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

            CreateMap<CreateCategoryDTO, Category>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? src.Image.GetHashCode() + "_" + src.Image.FileName : null));

            CreateMap<CreateAdvertismentDTO, Advertisment>()
                .ForMember(dest => dest.AdvertismentImages, opt => opt.Ignore());

            CreateMap<Advertisment, GetAdvertismentDTO>().ReverseMap();

            CreateMap<AdvertismentImage, GetAdvertismentImageDTO>().ReverseMap();
        }
    }
}