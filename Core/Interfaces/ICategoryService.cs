using Core.DTOs;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDTO>> GetAll();
        Task<IEnumerable<GetCategoryDTO>> GetHead();
        Task<GetCategoryDTO?> GetById(int id);
        Task<IEnumerable<GetCategoryDTO>> GetByParentId(int parentId);
        Task Create(CreateCategoryDTO category);
        Task Edit(int categoryId, CreateCategoryDTO category);
        Task Delete(int id);
    }
}
