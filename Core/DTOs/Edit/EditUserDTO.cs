using Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Core.DTOs
{
    public class EditUserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
