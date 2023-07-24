using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public string? ProfilePicture { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Advertisement> Advertisements { get; set; }
    }
}
