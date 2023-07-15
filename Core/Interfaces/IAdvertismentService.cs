using Core.DTOs;

namespace Core.Interfaces
{
    public interface IAdvertismentService
    {
        Task<IEnumerable<GetAdvertismentDTO>> GetAll();
        Task<GetAdvertismentDTO?> GetById(int id);
        Task<IEnumerable<GetAdvertismentDTO>> GetBySubcategoryId(int subcategoryId);
        Task<IEnumerable<GetAdvertismentDTO>> GetByCategoryId(int categoryId);
        Task Create(CreateAdvertismentDTO advertisment);
        Task Edit(int advertismentId, CreateAdvertismentDTO advertisment);
        Task Delete(int id);
    }
}
