using Core.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRoleService
    {
        Task CreateRole(string roleName);
        Task AddToRole(string userId, string roleName);
        Task RemoveFromRole(string userId, string roleName);
        Task<IEnumerable<IdentityRole>> GetAll();
    }
}
