using Core.DTOs;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDTO>> GetAll();
        Task<GetCategoryDTO?> GetById(int id);
        Task Create(CreateCategoryDTO category);
        Task Edit(int categoryId, CreateCategoryDTO category);
        Task Delete(int id);
    }
}
