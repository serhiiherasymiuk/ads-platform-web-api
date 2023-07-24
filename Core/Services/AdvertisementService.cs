using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Resources;
using Core.Specifications;
using System.Net;

namespace Core.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IRepository<Advertisement> advertisementsRepo;
        private readonly IRepository<AdvertisementImage> advertisementImagesRepo;
        private readonly IFileStorageService azureStorageService;
        private readonly IMapper mapper;

        public AdvertisementService(IRepository<Advertisement> advertisementsRepo, 
                                   IRepository<AdvertisementImage> advertisementImagesRepo,
                                   IFileStorageService azureStorageService,
                                   IMapper mapper)
        {
            this.advertisementsRepo = advertisementsRepo;
            this.advertisementImagesRepo = advertisementImagesRepo;
            this.azureStorageService = azureStorageService;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetAdvertisementDTO>> GetAll()
        {
            var advertisements = await advertisementsRepo.GetAllBySpec(new Advertisements.All());
            return mapper.Map<IEnumerable<GetAdvertisementDTO>>(advertisements);
        }
        public async Task<GetAdvertisementDTO?> GetById(int id)
        {
            Advertisement advertisement = await advertisementsRepo.GetBySpec(new Advertisements.ById(id));
            if (advertisement == null)
                throw new HttpException(ErrorMessages.AdvertisementByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<GetAdvertisementDTO>(advertisement);
        }
        public async Task<IEnumerable<GetAdvertisementDTO>> GetByCategoryId(int categoryId)
        {
            var advertisments = await advertisementsRepo.GetAllBySpec(new Advertisements.ByCategoryId(categoryId));
            return mapper.Map<IEnumerable<GetAdvertisementDTO>>(advertisments);
        }
        public async Task<IEnumerable<GetAdvertisementDTO>> GetByCategoryName(string categoryName)
        {
            var advertisments = await advertisementsRepo.GetAllBySpec(new Advertisements.ByCategoryName(categoryName));
            return mapper.Map<IEnumerable<GetAdvertisementDTO>>(advertisments);
        }
        public async Task Create(CreateAdvertisementDTO advertisement)
        {
            var newAdvertisement = mapper.Map<Advertisement>(advertisement);

            if (advertisement.AdvertisementImages != null && advertisement.AdvertisementImages.Any())
            {
                var imageEntities = new List<AdvertisementImage>();
                foreach (var imageFile in advertisement.AdvertisementImages)
                {
                    var imageEntity = new AdvertisementImage
                    {
                        Image = Path.GetRandomFileName(),
                        Advertisement = newAdvertisement
                    };
                    imageEntities.Add(imageEntity);

                    await azureStorageService.UploadFile("advertisement-images", imageFile, imageEntity.Image);
                }
                newAdvertisement.AdvertisementImages = imageEntities;
            }

            newAdvertisement.CreationDate = DateTime.Now.ToUniversalTime();

            await advertisementsRepo.Insert(newAdvertisement);
            await advertisementsRepo.Save();
        }
        public async Task Edit(int advertisementId, CreateAdvertisementDTO advertisement)
        {
            var existingAdvertisment = await advertisementsRepo.GetBySpec(new Advertisements.ById(advertisementId));
            if (existingAdvertisment == null)
                throw new HttpException(ErrorMessages.AdvertisementByIdNotFound, HttpStatusCode.NotFound);
            if (existingAdvertisment.AdvertisementImages != null && existingAdvertisment.AdvertisementImages.Any())
            {
                foreach (var image in existingAdvertisment.AdvertisementImages)
                {
                    await azureStorageService.DeleteFile("advertisement-images", image.Image);

                    await advertisementImagesRepo.Delete(image.Id);
                }
            }

            var advertismentEntity = mapper.Map<Advertisement>(advertisement);
            advertismentEntity.Id = advertisementId;
            await advertisementsRepo.Update(mapper.Map<Advertisement>(advertismentEntity));
            await advertisementsRepo.Save();

            if (advertisement.AdvertisementImages != null && advertisement.AdvertisementImages.Any())
            {
                foreach (var imageFile in advertisement.AdvertisementImages)
                {
                    var advertismentImage = new AdvertisementImage
                    {
                        Image = Path.GetRandomFileName(),
                        Advertisement = advertismentEntity
                    };
                    await advertisementImagesRepo.Insert(advertismentImage);

                    await azureStorageService.UploadFile("advertisement-images", imageFile, advertismentImage.Image);
                }
            }
            await advertisementImagesRepo.Save();
        }
        public async Task Delete(int id)
        {
            var advertisement = await advertisementsRepo.GetBySpec(new Advertisements.ById(id));
            if (advertisement == null)
                throw new HttpException(ErrorMessages.AdvertisementByIdNotFound, HttpStatusCode.NotFound);

            if (advertisement.AdvertisementImages != null && advertisement.AdvertisementImages.Any())
            {
                foreach (var image in advertisement.AdvertisementImages)
                {
                    await azureStorageService.DeleteFile("advertisement-images", image.Image);
                }
            }

            await advertisementsRepo.Delete(id);
            await advertisementsRepo.Save();
        }
        public async Task<IEnumerable<GetAdvertisementDTO>> Search(string query)
        {
            var advertisments = await advertisementsRepo.GetAllBySpec(new Advertisements.ByDateDescending());

            if (string.IsNullOrWhiteSpace(query))
            {
                return mapper.Map<IEnumerable<GetAdvertisementDTO>>(advertisments);
            }

            query = query.Trim().ToLower();

            var matchingAdvertisments = advertisments
                .Where(a => a.Name.ToLower().Contains(query) || a.Description.ToLower().Contains(query))
                .ToList();

            return mapper.Map<IEnumerable<GetAdvertisementDTO>>(matchingAdvertisments);
        }
    }
}