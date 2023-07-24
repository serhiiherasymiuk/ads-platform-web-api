namespace Core.Entities
{
    public class Advertisement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhoneNumber { get; set; }
        public decimal Price { get; set; } 
        public DateTime CreationDate { get; set; }
        public List<AdvertisementImage> AdvertisementImages { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
