﻿namespace Core.Entities
{
    public class Advertisment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhoneNumber { get; set; }
        public List<AdvertismentImage> AdvertismentImages { get; set; }
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}