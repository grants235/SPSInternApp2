using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GbayApiWebApplicationV2.Controllers
{
    
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext context;

        public AdministrationController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageUsers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageProductsAsync()
        {
            return View();
        }
    }
}