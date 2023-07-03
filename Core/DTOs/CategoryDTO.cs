using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public ICollection<SubcategoryDTO>? Subcategories { get; set; }
    }
}
