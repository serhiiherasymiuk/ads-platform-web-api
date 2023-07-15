using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
