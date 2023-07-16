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
    public class SubcategoryService : ISubcategoryService
    {
        private readonly IRepository<Subcategory> subcategoriesRepo;
        private readonly IMapper mapper;

        public SubcategoryService(IRepository<Subcategory> subcategoriesRepo, IMapper mapper)
        {
            this.subcategoriesRepo = subcategoriesRepo;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetSubcategoryDTO>> GetAll()
        {
            var subcategories = await subcategoriesRepo.GetAllBySpec(new Subcategories.All());
            return mapper.Map<IEnumerable<GetSubcategoryDTO>>(subcategories);
        }
        public async Task<GetSubcategoryDTO?> GetById(int id)
        {
            Subcategory subcategory = await subcategoriesRepo.GetBySpec(new Subcategories.ById(id));
            if (subcategory == null)
                throw new HttpException(ErrorMessages.SubcategoryByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<GetSubcategoryDTO>(subcategory);
        }
        public async Task Edit(int subcategoryId, CreateSubcategoryDTO subcategory)
        {
            var existingSubcategory = await subcategoriesRepo.GetByID(subcategoryId);
            if (existingSubcategory == null)
                throw new HttpException(ErrorMessages.CategoryByIdNotFound, HttpStatusCode.NotFound);

            var subcategoryEntity = mapper.Map<Subcategory>(subcategory);
            subcategoryEntity.Id = subcategoryId;

            await subcategoriesRepo.Update(subcategoryEntity);
            await subcategoriesRepo.Save();
        }
        public async Task Create(CreateSubcategoryDTO subcategory)
        {
            await subcategoriesRepo.Insert(mapper.Map<Subcategory>(subcategory));
            await subcategoriesRepo.Save();
        }

        public async Task Delete(int id)
        {
            if (await subcategoriesRepo.GetByID(id) == null)
                throw new HttpException(ErrorMessages.SubcategoryByIdNotFound, HttpStatusCode.NotFound);
            await subcategoriesRepo.Delete(id);
            await subcategoriesRepo .Save();
        }

        public async Task<GetSubcategoryDTO?> GetByCategoryId(int categoryId)
        {
            Subcategory subcategory = await subcategoriesRepo.GetBySpec(new Subcategories.ByCategoryId(categoryId));
            if (subcategory == null)
                throw new HttpException(ErrorMessages.SubcategoryByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<GetSubcategoryDTO>(subcategory);
        }
    }
}