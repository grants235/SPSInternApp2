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
    [Authorize]
    [Route("api/[controller]")]
    public class EditUserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public EditUserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]EditUserViewModel model)
        {
            ApplicationUser user = await userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.UserName = model.Username;
                user.Email = model.Email;
                user.SecurityQuestion1 = model.SecurityQuestion1;
                user.SecurityQuestion2 = model.SecurityQuestion2;
                if (model.Buyer == true)
                {
                    await userManager.AddToRoleAsync(user, "Buyers");
                }
                else
                {
                    await userManager.RemoveFromRoleAsync(user, "Buyers");
                }

                if (model.Seller == true)
                {
                    await userManager.AddToRoleAsync(user, "Sellers");
                }
                else
                {
                    await userManager.RemoveFromRoleAsync(user, "Sellers");
                }

                if (model.Moderator == true)
                {
                    await userManager.AddToRoleAsync(user, "Moderators");
                }
                else
                {
                    await userManager.RemoveFromRoleAsync(user, "Moderators");
                }

                if (model.Administrator == true)
                {
                    await userManager.AddToRoleAsync(user, "Administrators");
                }
                else
                {
                    await userManager.RemoveFromRoleAsync(user, "Administrators");
                }

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new OkResult();
                }
            }
            return new UnauthorizedResult();
        }


    }
}
