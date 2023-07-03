using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class AdvertismentImageDTO
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int AdvertismentId { get; set; }
    }
}
