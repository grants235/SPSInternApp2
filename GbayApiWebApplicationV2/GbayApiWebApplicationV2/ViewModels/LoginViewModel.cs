﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GbayApiWebApplicationV2.ViewModels
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
