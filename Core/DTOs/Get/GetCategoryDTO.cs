using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<GetSubcategoryDTO>? Subcategories { get; set; }
    }
}
