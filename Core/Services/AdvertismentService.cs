using Core.Interfaces;
using Core.Entities;
using Core.Specifications;
using AutoMapper;
using Core.Helpers;
using System.Net;
using Core.Resources;
using Core.DTOs;

namespace Core.Services
{
    public class AdvertismentService : IAdvertismentService
    {
        private readonly IRepository<Advertisment> advertismentsRepo;
        private readonly IRepository<AdvertismentImage> advertismentImagesRepo;
        private readonly IFileStorageService azureStorageService;
        private readonly IMapper mapper;

        public AdvertismentService(IRepository<Advertisment> advertismentsRepo, 
                                   IRepository<AdvertismentImage> advertismentImagesRepo,
                                   IFileStorageService azureStorageService,
                                   IMapper mapper)
        {
            this.advertismentsRepo = advertismentsRepo;
            this.advertismentImagesRepo = advertismentImagesRepo;
            this.azureStorageService = azureStorageService;
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
        public async Task<IEnumerable<GetAdvertismentDTO>> GetByCategoryId(int categoryId)
        {
            var advertisments = await advertismentsRepo.GetAllBySpec(new Advertisments.ByCategoryId(categoryId));
            return mapper.Map<IEnumerable<GetAdvertismentDTO>>(advertisments);
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
                        Image = Path.GetRandomFileName(),
                        Advertisment = newAdvertisment
                    };
                    imageEntities.Add(imageEntity);

                    await azureStorageService.UploadFile("advertisment-images", imageFile, imageEntity.Image);
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
                    await azureStorageService.DeleteFile("advertisment-images", image.Image);

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
                        Image = Path.GetRandomFileName(),
                        Advertisment = advertismentEntity
                    };
                    await advertismentImagesRepo.Insert(advertismentImage);

                    await azureStorageService.UploadFile("advertisment-images", imageFile, advertismentImage.Image);
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
                    await azureStorageService.DeleteFile("advertisment-images", image.Image);
                }
            }

            await advertismentsRepo.Delete(id);
            await advertismentsRepo.Save();
        }
    }
}