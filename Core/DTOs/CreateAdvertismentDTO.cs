using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CreateAdvertismentDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhoneNumber { get; set; }
        public decimal Price { get; set; }
        public List<IFormFile>? AdvertismentImages { get; set; }
        public int SubcategoryId { get; set; }
        public string UserId { get; set; }
    }
}
