using Core.Interfaces;
using Core.Entities;
using Core.Specifications;
using Core.DTOs;
using AutoMapper;
using Core.Helpers;
using System.Net;
using Core.Resources;

namespace Core.Services
{
    public class AdvertismentService : IAdvertismentService
    {
        private readonly IRepository<Advertisment> advertismentsRepo;
        private readonly IMapper mapper;

        public AdvertismentService(IRepository<Advertisment> advertismentsRepo, IMapper mapper)
        {
            this.advertismentsRepo = advertismentsRepo;
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
            await advertismentsRepo.Insert(mapper.Map<Advertisment>(advertisment));
            await advertismentsRepo.Save();
        }
        public async Task Edit(CreateAdvertismentDTO advertisment)
        {
            await advertismentsRepo.Update(mapper.Map<Advertisment>(advertisment));
            await advertismentsRepo.Save();
        }

        public async Task Delete(int id)
        {
            if (await advertismentsRepo.GetByID(id) == null)
                throw new HttpException(ErrorMessages.AdvertismentByIdNotFound, HttpStatusCode.NotFound);
            await advertismentsRepo.Delete(id);
            await advertismentsRepo.Save();
        }
    }
}