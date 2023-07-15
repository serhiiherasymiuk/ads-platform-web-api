using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces
{
    public interface IRoleService
    {
        Task Create(string roleName);
        Task Delete(string roleName);
        Task AddToRole(string userId, string roleName);
        Task RemoveFromRole(string userId, string roleName);
        Task<IEnumerable<IdentityRole>> GetAll();
    }
}
