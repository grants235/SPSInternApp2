using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Data;
using GbayApiWebApplicationV2.Models;
using GbayApiWebApplicationV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GbayApiWebApplicationV2.ApiControllers
{
    [Route("api/[controller]")]
    public class EditProductController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public EditProductController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]EditProductViewModel model)
        {
            ApplicationUser user = await userManager.FindByIdAsync(User.Identity.Name);
            if (user != null)
            {
                Product product = await context.Products.FindAsync(model.id);
                if (product != null)
                {
                    if (product.Seller == user.UserName)
                    {
                        product.ProductName = model.productName;
                        product.Description = model.productDescription;
                        product.Price = model.productPrice;
                        product.ImgUrl = model.productImgUrl;
                        context.Products.Update(product);
                        return new OkResult();
                    }
                }
            }
            return new UnauthorizedResult();
        }

    }
}
