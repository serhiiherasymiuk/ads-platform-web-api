using Core.DTOs;

namespace Core.Interfaces
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<GetSubcategoryDTO>> GetAll();
        Task<GetSubcategoryDTO?> GetById(int id);
        Task<GetSubcategoryDTO?> GetByCategoryId(int categoryId);
        Task Create(CreateSubcategoryDTO subcategory);
        Task Edit(int subcategoryId, CreateSubcategoryDTO subcategory);
        Task Delete(int id);
    }
}
