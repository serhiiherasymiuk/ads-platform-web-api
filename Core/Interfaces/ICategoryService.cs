using Core.DTOs;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<CategoryDTO?> GetById(int id);
        Task Create(CategoryDTO category);
        Task Edit(CategoryDTO category);
        Task Delete(int id);
    }
}
