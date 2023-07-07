using Core.DTOs;

namespace Core.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> GetById(string id);
        Task<LoginResponseDTO> Login(LoginDTO loginDTO);
        Task Register(RegisterDTO registerDTO);
        Task Logout();
        Task Delete(string id);
    }
}