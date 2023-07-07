using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task CreateRole(string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                await roleManager.CreateAsync(role);
            }
        }
        public async Task AddToRole(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }

        public async Task<IEnumerable<IdentityRole>> GetAll()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return roles;
        }
    }
}
