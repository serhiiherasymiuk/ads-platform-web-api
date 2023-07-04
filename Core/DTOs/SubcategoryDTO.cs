﻿using Core.Entities;

namespace Core.DTOs
{
    public class SubcategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public ICollection<AdvertismentDTO>? Advertisments { get; set; }
    }
}