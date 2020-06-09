using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GbayApiWebApplicationV2.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public decimal Credits { get; set; }
    }
}
