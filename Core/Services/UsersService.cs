using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;
using Core.Specifications;

namespace Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;
        private readonly IJwtService jwtService;

        public UsersService(UserManager<User> userManager,
                            SignInManager<User> signInManager,
                            IMapper mapper,
                            IJwtService jwtService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.jwtService = jwtService;;
        }
        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await userManager.Users.ToListAsync();
            return mapper.Map<IEnumerable<UserDTO>>(users);
        }
        public async Task<UserDTO> GetById(string id)
        {
            var user = await userManager.Users.Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (user == null)
                throw new HttpException(ErrorMessages.UserByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<UserDTO>(user);
        }
        public async Task<LoginResponseDTO> Login(LoginDTO login)
        {
            var user = await userManager.FindByNameAsync(login.Username);
            if (user == null || !await userManager.CheckPasswordAsync(user, login.Password))
                throw new HttpException(ErrorMessages.InvalidCreds, HttpStatusCode.BadRequest);
            await signInManager.SignInAsync(user, true);

            return new LoginResponseDTO()
            {
                Token = jwtService.CreateToken(jwtService.GetClaims(user))
            };
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task Register(RegisterDTO register)
        {
            User user = new()
            {
                UserName = register.UserName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                RegistrationDate = DateTime.Now,
            };

            var result = await userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                string message = string.Join(", ", result.Errors.Select(x => x.Description));

                throw new HttpException(message, HttpStatusCode.BadRequest);
            }

            await userManager.AddToRoleAsync(user, "User");
        }

        public async Task Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                throw new HttpException(ErrorMessages.UserByIdNotFound, HttpStatusCode.NotFound);

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                string message = string.Join(", ", result.Errors.Select(x => x.Description));
                throw new HttpException(message, HttpStatusCode.BadRequest);
            }
        }

        public async Task Edit(UserDTO userDto)
        {
            var user = await userManager.FindByIdAsync(userDto.Id);
            if (user == null)
                throw new HttpException(ErrorMessages.UserByIdNotFound, HttpStatusCode.NotFound);
            mapper.Map(userDto, user);
            await userManager.UpdateAsync(user);
        }
    }
}