﻿using System;
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
    public class SecurityQuestionController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public SecurityQuestionController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
       
        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]LoginViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Email))
            {
                return new UnauthorizedResult();
            }

            ApplicationUser user = await userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                if (user.Email == model.Email && user.SecurityQuestion1 == model.SecurityQuestion1 && user.SecurityQuestion2 == model.SecurityQuestion2)
                {
                    return new OkResult();
                }
            }

            return new UnauthorizedResult();
        }
    }
}
