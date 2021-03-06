﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GbayApiWebApplicationV2.ViewModels
{
    public class ManageUsersViewModel
    {


        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string SecurityQuestion1 { get; set; }
        public string SecurityQuestion2 { get; set; }
        public bool EmailConfirmed { get; set; }
        public IList<string> Roles { get; set; }
    }
}
