using Core.Entities;

namespace Core.DTOs
{
    public class GetSubcategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public ICollection<GetAdvertismentDTO>? Advertisments { get; set; }
    }
}
