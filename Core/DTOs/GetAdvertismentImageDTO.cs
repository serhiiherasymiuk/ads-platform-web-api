using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class GetAdvertismentImageDTO
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int AdvertismentId { get; set; }
    }
}
