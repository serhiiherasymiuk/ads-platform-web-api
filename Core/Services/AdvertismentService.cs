using Core.Interfaces;
using Core.Entities;
using Core.Specifications;
using Core.DTOs;
using AutoMapper;
using Core.Helpers;
using System.Net;
using Core.Resources;
using static System.Net.Mime.MediaTypeNames;
using Ardalis.Specification;
using Microsoft.AspNetCore.Hosting.Server;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;

namespace Core.Services
{
    public class AdvertismentService : IAdvertismentService
    {
        private readonly IRepository<Advertisment> advertismentsRepo;
        private readonly IRepository<AdvertismentImage> advertismentImagesRepo;
        private readonly IMapper mapper;

        public AdvertismentService(IRepository<Advertisment> advertismentsRepo, 
                                   IRepository<AdvertismentImage> advertismentImagesRepo, 
                                   IMapper mapper)
        {
            this.advertismentsRepo = advertismentsRepo;
            this.advertismentImagesRepo = advertismentImagesRepo;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetAdvertismentDTO>> GetAll()
        {
            var advertisments = await advertismentsRepo.GetAllBySpec(new Advertisments.All());
            return mapper.Map<IEnumerable<GetAdvertismentDTO>>(advertisments);
        }
        public async Task<GetAdvertismentDTO?> GetById(int id)
        {
            Advertisment advertisment = await advertismentsRepo.GetBySpec(new Advertisments.ById(id));
            if (advertisment == null)
                throw new HttpException(ErrorMessages.AdvertismentByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<GetAdvertismentDTO>(advertisment);
        }
        public async Task<GetAdvertismentDTO?> GetBySubcategoryId(int id)
        {
            Advertisment advertisment = await advertismentsRepo.GetBySpec(new Advertisments.BySubcategoryId(id));
            if (advertisment == null)
                throw new HttpException(ErrorMessages.AdvertismentByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<GetAdvertismentDTO>(advertisment);
        }
        public async Task Create(CreateAdvertismentDTO advertisment)
        {
            var newAdvertisment = mapper.Map<Advertisment>(advertisment);

            if (advertisment.AdvertismentImages != null && advertisment.AdvertismentImages.Any())
            {
                var imageEntities = new List<AdvertismentImage>();
                foreach (var imageFile in advertisment.AdvertismentImages)
                {
                    var imageEntity = new AdvertismentImage
                    {
                        Image = imageFile.GetHashCode() + "_" + imageFile.FileName,
                        Advertisment = newAdvertisment
                    };
                    var imagePath = Path.Combine("uploads", imageEntity.Image);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    imageEntities.Add(imageEntity);
                }
                newAdvertisment.AdvertismentImages = imageEntities;
            }

            await advertismentsRepo.Insert(newAdvertisment);
            await advertismentsRepo.Save();
        }
        public async Task Edit(int advertismentId, CreateAdvertismentDTO advertisment)
        {
            var existingAdvertisment = await advertismentsRepo.GetBySpec(new Advertisments.ById(advertismentId));
            if (existingAdvertisment == null)
                throw new HttpException(ErrorMessages.AdvertismentByIdNotFound, HttpStatusCode.NotFound);
            if (existingAdvertisment.AdvertismentImages != null && existingAdvertisment.AdvertismentImages.Any())
            {
                foreach (var image in existingAdvertisment.AdvertismentImages)
                {
                    var imagePath = Path.Combine("uploads", image.Image);

                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }

                    await advertismentImagesRepo.Delete(image.Id);
                }
            }

            var advertismentEntity = mapper.Map<Advertisment>(advertisment);
            advertismentEntity.Id = advertismentId;
            await advertismentsRepo.Update(mapper.Map<Advertisment>(advertismentEntity));
            await advertismentsRepo.Save();

            if (advertisment.AdvertismentImages != null && advertisment.AdvertismentImages.Any())
            {
                foreach (var imageFile in advertisment.AdvertismentImages)
                {
                    var advertismentImage = new AdvertismentImage
                    {
                        Image = imageFile.GetHashCode() + "_" + imageFile.FileName,
                        Advertisment = advertismentEntity
                    };
                    var imagePath = Path.Combine("uploads", advertismentImage.Image);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    await advertismentImagesRepo.Insert(advertismentImage);
                }
            }
            await advertismentImagesRepo.Save();
        }
        public async Task Delete(int id)
        {
            var advertisment = await advertismentsRepo.GetBySpec(new Advertisments.ById(id));
            if (advertisment == null)
                throw new HttpException(ErrorMessages.AdvertismentByIdNotFound, HttpStatusCode.NotFound);

            if (advertisment.AdvertismentImages != null && advertisment.AdvertismentImages.Any())
            {
                foreach (var image in advertisment.AdvertismentImages)
                {
                    var imagePath = Path.Combine("uploads", image.Image);

                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
            }

            await advertismentsRepo.Delete(id);
            await advertismentsRepo.Save();
        }
    }
}