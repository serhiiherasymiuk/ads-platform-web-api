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
    public class SubcategoryService : ISubcategoryService
    {
        private readonly IRepository<Subcategory> subcategoriesRepo;
        private readonly IMapper mapper;

        public SubcategoryService(IRepository<Subcategory> subcategoriesRepo, IMapper mapper)
        {
            this.subcategoriesRepo = subcategoriesRepo;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<SubcategoryDTO>> GetAll()
        {
            var subcategories = await subcategoriesRepo.GetAllBySpec(new Subcategories.All());
            return mapper.Map<IEnumerable<SubcategoryDTO>>(subcategories);
        }
        public async Task<SubcategoryDTO?> GetById(int id)
        {
            Subcategory subcategory = await subcategoriesRepo.GetBySpec(new Subcategories.ById(id));
            if (subcategory == null)
                throw new HttpException(ErrorMessages.SubcategoryByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<SubcategoryDTO>(subcategory);
        }
        public async Task Edit(SubcategoryDTO subcategory)
        {
            await subcategoriesRepo.Update(mapper.Map<Subcategory>(subcategory));
            await subcategoriesRepo.Save();
        }
        public async Task Create(SubcategoryDTO subcategory)
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

        public async Task<SubcategoryDTO?> GetByCategoryId(int categoryId)
        {
            Subcategory subcategory = await subcategoriesRepo.GetBySpec(new Subcategories.ByCategoryId(categoryId));
            if (subcategory == null)
                throw new HttpException(ErrorMessages.SubcategoryByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<SubcategoryDTO>(subcategory);
        }
    }
}