using Core.DTOs;

namespace Core.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<GetUserDTO>> GetAll();
        Task<GetUserDTO> GetById(string id);
        Task<LoginResponseDTO> Login(LoginDTO loginDTO);
        Task Register(RegisterDTO registerDTO);
        Task Logout();
        Task Delete(string id);
        Task Edit(string userId, EditUserDTO user);
        Task<bool> CheckUsernameExists(string userName);
        Task<bool> CheckEmailExists(string email);
    }
}