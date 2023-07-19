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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoriesRepo;
        private readonly IFileStorageService azureStorageService;
        private readonly IMapper mapper;

        public CategoryService(IRepository<Category> categoriesRepo, IFileStorageService azureStorageService, IMapper mapper)
        {
            this.categoriesRepo = categoriesRepo;
            this.azureStorageService = azureStorageService;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetCategoryDTO>> GetAll()
        {
            var categories = await categoriesRepo.GetAllBySpec(new Categories.All());
            return mapper.Map<IEnumerable<GetCategoryDTO>>(categories);
        }
        public async Task<IEnumerable<GetCategoryDTO>> GetHead()
        {
            var categories = await categoriesRepo.GetAllBySpec(new Categories.Head());
            return mapper.Map<IEnumerable<GetCategoryDTO>>(categories);
        }
        public async Task<GetCategoryDTO?> GetById(int id)
        {
            Category category = await categoriesRepo.GetBySpec(new Categories.ById(id));
            if (category == null)
                throw new HttpException(ErrorMessages.CategoryByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<GetCategoryDTO>(category);
        }
        public async Task<IEnumerable<GetCategoryDTO>> GetByParentId(int parentId)
        {
            var categories = await categoriesRepo.GetAllBySpec(new Categories.ByParentId(parentId));
            return mapper.Map<IEnumerable<GetCategoryDTO>>(categories);
        }
        public async Task Edit(int categoryId, EditCategoryDTO category)
        {
            var existingCategory = await categoriesRepo.GetByID(categoryId);
            if (existingCategory == null)
                throw new HttpException(ErrorMessages.CategoryByIdNotFound, HttpStatusCode.NotFound);

            var categoryEntity = mapper.Map<Category>(category);
            categoryEntity.Id = categoryId;

            if (category.Image == null)
                categoryEntity.Image = existingCategory.Image;
            else 
                await azureStorageService.EditFile("category-images", existingCategory.Image, category.Image);

            await categoriesRepo.Update(categoryEntity);
            await categoriesRepo.Save();
        }
        public async Task Create(CreateCategoryDTO category)
        {
            var categoryEntity = mapper.Map<Category>(category);

            await azureStorageService.UploadFile("category-images", category.Image);

            await categoriesRepo.Insert(categoryEntity);
            await categoriesRepo.Save();
        }

        public async Task Delete(int id)
        {
            var category = await categoriesRepo.GetByID(id);
            if (category == null)
                throw new HttpException(ErrorMessages.CategoryByIdNotFound, HttpStatusCode.NotFound);

            await azureStorageService.DeleteFile("category-images", category.Image);

            await categoriesRepo.Delete(id);
            await categoriesRepo.Save();
        }

    }
}