using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Core.DTOs;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await usersService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            return Ok(await usersService.GetById(id));
        }
        [HttpGet("getByUserName/{userName}")]
        public async Task<IActionResult> GetByUserName([FromRoute] string userName)
        {
            return Ok(await usersService.GetByUserName(userName));
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            await usersService.Register(register);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var response = await usersService.Login(login);
            return Ok(response);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await usersService.Logout();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await usersService.Delete(id);
            return Ok();
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> Edit(string userId, [FromForm] EditUserDTO user)
        {
            await usersService.Edit(userId, user);
            return Ok();
        }
        [HttpGet("checkUsernameExists/{userName}")]
        public async Task<IActionResult> CheckUsernameExists([FromRoute] string userName)
        {
            return Ok(await usersService.CheckUsernameExists(userName));
        }
        [HttpGet("checkEmailExists/{email}")]
        public async Task<IActionResult> CheckEmailExists([FromRoute] string email)
        {
            return Ok(await usersService.CheckEmailExists(email));
        }
    }
}
