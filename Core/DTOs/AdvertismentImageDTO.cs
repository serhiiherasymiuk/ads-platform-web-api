using Core.Entities;
using Microsoft.AspNetCore.Http;
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
        public IFormFile Image { get; set; }
        public int AdvertismentId { get; set; }
    }
}
