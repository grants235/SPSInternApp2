using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Data;
using GbayApiWebApplicationV2.Models;
using GbayApiWebApplicationV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GbayApiWebApplicationV2.ApiControllers
{
    [Route("api/[controller]")]
    public class ManageProductsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public ManageProductsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var user = await userManager.FindByIdAsync(User.Identity.Name);
            if (user != null)
            {
                var products = await context.Products.ToListAsync();
                ManageProductsViewModel model = new ManageProductsViewModel();
                foreach (var product in products)
                {
                    if (product.Seller == user.UserName)
                    {
                        model.Products.Add(product);
                    }
                }
                return new OkObjectResult(model);
            }
            return new UnauthorizedResult();
        }

        
    }
}
