using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IFileStorageService, AzureStorageService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAdvertismentService, AdvertismentService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IRoleService, RoleService>();
        }
    }
}