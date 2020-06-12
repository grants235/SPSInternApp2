using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Models;
using GbayApiWebApplicationV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GbayApiWebApplicationV2.ApiControllers
{
    [Route("api/[controller]")]
    public class ResetPasswordController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ResetPasswordController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]ResetPasswordViewModel model)
        {
            if (model.Password == model.ConfirmPassword)
            {
                var user = await userManager.FindByIdAsync(model.UserId);
                if (user != null){
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return new OkResult();
                    }
                }
            }
            return new UnauthorizedResult();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
