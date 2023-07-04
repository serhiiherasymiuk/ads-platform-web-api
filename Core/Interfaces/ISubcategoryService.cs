using Core.DTOs;

namespace Core.Interfaces
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<SubcategoryDTO>> GetAll();
        Task<SubcategoryDTO?> GetById(int id);
        Task<SubcategoryDTO?> GetByCategoryId(int categoryId);
        Task Create(SubcategoryDTO subcategory);
        Task Edit(SubcategoryDTO subcategory);
        Task Delete(int id);
    }
}
