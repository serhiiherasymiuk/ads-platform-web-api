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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoriesRepo;
        private readonly IMapper mapper;

        public CategoryService(IRepository<Category> categoryRepo, IMapper mapper)
        {
            this.categoriesRepo = categoryRepo;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = await categoriesRepo.GetAllBySpec(new Categories.All());
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
        public async Task<CategoryDTO?> GetById(int id)
        {
            Category category = await categoriesRepo.GetBySpec(new Categories.ById(id));
            if (category == null)
                throw new HttpException(ErrorMessages.CategoryByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<CategoryDTO>(category);
        }
        public async Task Edit(CategoryDTO category)
        {
            await categoriesRepo.Update(mapper.Map<Category>(category));
            await categoriesRepo.Save();
        }
        public async Task Create(CategoryDTO category)
        {
            await categoriesRepo.Insert(mapper.Map<Category>(category));
            await categoriesRepo.Save();
        }
        public async Task Delete(int id)
        {
            if (await categoriesRepo.GetByID(id) == null)
                throw new HttpException(ErrorMessages.CategoryByIdNotFound, HttpStatusCode.NotFound);
            await categoriesRepo.Delete(id);
            await categoriesRepo.Save();
        }
    }
}