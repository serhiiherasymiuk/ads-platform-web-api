using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;

        public JwtService(IConfiguration configuration, UserManager<User> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public string CreateToken(IEnumerable<Claim> claims)
        {
            var jwtOpts = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpts.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtOpts.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtOpts.Lifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.id, user.Id),
                new Claim(CustomClaimTypes.userName, user.UserName),
                new Claim(CustomClaimTypes.email, user.Email),
                new Claim(CustomClaimTypes.profilePicture, user.ProfilePicture ?? ""),
                new Claim(CustomClaimTypes.registrationDate, user.RegistrationDate.ToString())
            };

            var roles = userManager.GetRolesAsync(user).Result;
            claims.AddRange(roles.Select(role => new Claim(CustomClaimTypes.roles, role)));

            return claims;
        }
    }

    public static class CustomClaimTypes
    {
        public const string id = "id";
        public const string userName = "userName";
        public const string email = "email";
        public const string roles = "roles";
        public const string profilePicture = "profilePicture";
        public const string registrationDate = "registrationDate";
    }
}