using Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Core.DTOs
{
    public class GetUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<GetAdvertismentDTO>? Advertisments { get; set; }
    }
}
