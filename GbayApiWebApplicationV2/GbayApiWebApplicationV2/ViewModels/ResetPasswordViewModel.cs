using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GbayApiWebApplicationV2.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
