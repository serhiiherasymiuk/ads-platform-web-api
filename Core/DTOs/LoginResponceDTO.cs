﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}