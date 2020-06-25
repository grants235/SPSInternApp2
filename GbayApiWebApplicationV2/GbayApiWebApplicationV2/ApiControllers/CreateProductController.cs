using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Data;
using GbayApiWebApplicationV2.Models;
using GbayApiWebApplicationV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GbayApiWebApplicationV2.ApiControllers
{
    [Route("api/[controller]")]
    public class CreateProductController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public CreateProductController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromHeader]string ProductName, [FromHeader]string ProductDescription, [FromHeader]decimal ProductPrice, [FromHeader]string ProductImgUrl)
        {
            var user = await userManager.FindByIdAsync(User.Identity.Name);
            if (user != null)
            {
                var RoleCheck = await userManager.IsInRoleAsync(user, "Sellers");
                if (RoleCheck == true)
                {
                    Product product = new Product
                    {
                        ProductName = ProductName,
                        Price = ProductPrice,
                        Description = ProductDescription,
                        ImgUrl = ProductImgUrl,
                        Seller = user.UserName
                    };
                    context.Products.Add(product);
                    await context.SaveChangesAsync();
                    return new OkResult();
                }
            }
            return new UnauthorizedResult();
        }

    }
}
