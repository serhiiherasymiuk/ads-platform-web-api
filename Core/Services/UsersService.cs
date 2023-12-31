﻿using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;

namespace Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IFileStorageService azureStorageService;
        private readonly IMapper mapper;
        private readonly IJwtService jwtService;

        public UsersService(UserManager<User> userManager,
                            SignInManager<User> signInManager,
                            IFileStorageService azureStorageService,
                            IMapper mapper,
                            IJwtService jwtService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.azureStorageService = azureStorageService;
            this.mapper = mapper;
            this.jwtService = jwtService;
        }
        public async Task<IEnumerable<GetUserDTO>> GetAll()
        {
            var users = await userManager.Users.ToListAsync();
            return mapper.Map<IEnumerable<GetUserDTO>>(users);
        }
        public async Task<GetUserDTO> GetById(string id)
        {
            var user = await userManager.Users.Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (user == null)
                throw new HttpException(ErrorMessages.UserByIdNotFound, HttpStatusCode.NotFound);

            var userDto = mapper.Map<GetUserDTO>(user);

            userDto.Roles = (List<string>)await userManager.GetRolesAsync(user);

            return userDto;
        }
        public async Task<GetUserDTO> GetByUserName(string userName)
        {
            var user = await userManager.Users.Where(u => u.UserName == userName)
                .FirstOrDefaultAsync();

            if (user == null)
                throw new HttpException(ErrorMessages.UserByUserNameNotFound, HttpStatusCode.NotFound);

            var userDto = mapper.Map<GetUserDTO>(user);

            userDto.Roles = (List<string>)await userManager.GetRolesAsync(user);

            return userDto;
        }
        public async Task<LoginResponseDTO> Login(LoginDTO login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
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
            if (register.Password != register.PasswordConfirmation)
                throw new HttpException(ErrorMessages.PasswordsNotMatch, HttpStatusCode.BadRequest);

            User user = new()
            {
                UserName = register.UserName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                RegistrationDate = DateTime.Now.ToUniversalTime(),
                ProfilePicture = "user-default-image.png",
            };

            var result = await userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                string message = string.Join(", ", result.Errors.Select(x => x.Description));

                throw new HttpException(message, HttpStatusCode.BadRequest);
            }

            await userManager.AddToRoleAsync(user, "user");
        }

        public async Task Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                throw new HttpException(ErrorMessages.UserByIdNotFound, HttpStatusCode.NotFound);

            if (user.ProfilePicture != "user-default-image.png")
                await azureStorageService.DeleteFile("user-images", user.ProfilePicture);

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                string message = string.Join(", ", result.Errors.Select(x => x.Description));
                throw new HttpException(message, HttpStatusCode.BadRequest);
            }
        }

        public async Task Edit(string userId, EditUserDTO userDto)
        {
            var user = await userManager.FindByIdAsync(userId);
            string oldFileName = user.ProfilePicture;
            mapper.Map(userDto, user);
            if (userDto.ProfilePicture != null)
                await azureStorageService.EditFile("user-images", oldFileName, user.ProfilePicture, userDto.ProfilePicture);
            else
                user.ProfilePicture = oldFileName;
            await userManager.UpdateAsync(user);
        }

        public async Task<bool> CheckUsernameExists(string userName)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return user != null;
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }
    }
}