using Core.DTOs;

namespace Core.Interfaces
{
    public interface IAdvertismentService
    {
        Task<IEnumerable<GetAdvertismentDTO>> GetAll();
        Task<GetAdvertismentDTO?> GetById(int id);
        Task<GetAdvertismentDTO?> GetBySubcategoryId(int subcategoryId);
        Task Create(CreateAdvertismentDTO advertisment);
        Task Edit(CreateAdvertismentDTO advertisment);
        Task Delete(int id);
    }
}
