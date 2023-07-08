using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;
        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            await roleService.CreateRole(roleName);
            return Ok();
        }
        [HttpPost("addToRole")]
        public async Task<IActionResult> AddToRole(string userId, string roleName)
        {
            await roleService.AddToRole(userId, roleName);
            return Ok();
        }
        [HttpPost("removeFromRole")]
        public async Task<IActionResult> RemoveFromRole(string userId, string roleName)
        {
            await roleService.RemoveFromRole(userId, roleName);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await roleService.GetAll());
        }
    }
}
