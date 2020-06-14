using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GbayApiWebApplicationV2.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GbayApiWebApplicationV2.ApiControllers
{
    [Route("api/[controller]")]
    public class AuthorizeCheckController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AuthorizeCheckController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET: api/<controller>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            ApplicationUser user = await userManager.FindByIdAsync(User.Identity.Name);
            if (user != null)
            {
                AuthorizeCheckViewModel model = new AuthorizeCheckViewModel
                {
                    Username = user.UserName
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
