using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GbayApiWebApplicationV2.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public bool Buyer { get; set; }
        public bool Seller { get; set; }
        public bool Moderator { get; set; }
        public bool Administrator { get; set; }
    }
}
