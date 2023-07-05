namespace Core.Entities
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Advertisment> Advertisments { get; set; }
    }
}
