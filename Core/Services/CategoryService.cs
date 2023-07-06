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

        public CategoryService(IRepository<Category> categoriesRepo, IMapper mapper)
        {
            this.categoriesRepo = categoriesRepo;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetCategoryDTO>> GetAll()
        {
            var categories = await categoriesRepo.GetAllBySpec(new Categories.All());
            return mapper.Map<IEnumerable<GetCategoryDTO>>(categories);
        }
        public async Task<GetCategoryDTO?> GetById(int id)
        {
            Category category = await categoriesRepo.GetBySpec(new Categories.ById(id));
            if (category == null)
                throw new HttpException(ErrorMessages.CategoryByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<GetCategoryDTO>(category);
        }
        public async Task Edit(int categoryId, CreateCategoryDTO category)
        {
            var existingCategory = await categoriesRepo.GetByID(categoryId);
            if (existingCategory == null)
                throw new HttpException(ErrorMessages.CategoryByIdNotFound, HttpStatusCode.NotFound);

            if (!string.IsNullOrEmpty(existingCategory.Image))
            {
                var oldImagePath = Path.Combine("uploads", existingCategory.Image);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            var categoryEntity = mapper.Map<Category>(category);
            categoryEntity.Id = categoryId;
            if (category.Image != null)
            {
                var newImagePath = Path.Combine("uploads", categoryEntity.Image);
                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await category.Image.CopyToAsync(stream);
                }
            }

            await categoriesRepo.Update(categoryEntity);
            await categoriesRepo.Save();
        }
        public async Task Create(CreateCategoryDTO category)
        {
            var categoryEntity = mapper.Map<Category>(category);

            var imagePath = Path.Combine("uploads", categoryEntity.Image);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await category.Image.CopyToAsync(stream);
            }

            await categoriesRepo.Insert(categoryEntity);
            await categoriesRepo.Save();
        }

        public async Task Delete(int id)
        {
            var category = await categoriesRepo.GetByID(id);
            if (category == null)
                throw new HttpException(ErrorMessages.CategoryByIdNotFound, HttpStatusCode.NotFound);

            var imagePath = Path.Combine("uploads", category.Image);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            await categoriesRepo.Delete(id);
            await categoriesRepo.Save();
        }

    }
}