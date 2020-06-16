using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Models;
using GbayApiWebApplicationV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GbayApiWebApplicationV2.ApiControllers
{
    [Route("api/[controller]")]
    public class MyAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public MyAccountController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET: api/<controller>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            ApplicationUser user = await userManager.FindByIdAsync(User.Identity.Name);
            if (user != null)
            {
                MyAccountViewModel model = new MyAccountViewModel
                {
                    Username = user.UserName,
                    Email = user.Email
                };
                return new OkObjectResult(model);
            }
            else
            {
                return new OkResult();
            }
        }

       
    }
}
