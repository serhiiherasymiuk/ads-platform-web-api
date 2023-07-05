using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class GetAdvertismentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhoneNumber { get; set; }
        public ICollection<AdvertismentImageDTO>? AdvertismentImages { get; set; }
        public int SubcategoryId { get; set; }
        public int UserId { get; set; }
    }
}
