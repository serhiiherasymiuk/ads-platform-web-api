﻿namespace Core.DTOs
{
    public class GetUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<GetAdvertisementDTO>? Advertisements { get; set; }
    }
}
