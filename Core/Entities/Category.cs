namespace Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public List<Advertisement> Advertisements { get; set; }
    }
}
