using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Models;
using GbayApiWebApplicationV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GbayApiWebApplicationV2.ApiControllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LoginViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Email))
            {
                return new UnauthorizedResult();
            }
            
            ApplicationUser user = await userManager.FindByNameAsync(model.Username);
            
            if (user != null)
            {
                if (user.Email == model.Email)
                {
                    if (user.SecurityQuestion1 != null && user.SecurityQuestion2 != null)
                    {
                        return new OkResult();
                    }
                    else
                    {
                        return new NoContentResult();
                    }
                }
            }
            
            return new UnauthorizedResult();

        }
    }
}
