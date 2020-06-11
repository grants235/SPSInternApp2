using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Models;
using GbayApiWebApplicationV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GbayApiWebApplicationV2.ApiControllers
{
    [Route("api/[controller]")]
    public class SetupSecurityQuestionsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public SetupSecurityQuestionsController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
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
                var result = await userManager.CheckPasswordAsync(user, model.Password);
                if (result == true && user.Email == model.Email)
                {
                    user.SecurityQuestion1 = model.SecurityQuestion1;
                    user.SecurityQuestion2 = model.SecurityQuestion2;
                    await userManager.UpdateAsync(user);

                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtKey"]));
                    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                                new Claim(ClaimTypes.Name, user.Id.ToString()),
                                new Claim(ClaimTypes.Role, "User")
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                    };
                    SecurityToken securityToken = handler.CreateToken(tokenDescriptor);
                    LoginResponseViewModel responseModel = new LoginResponseViewModel();
                    responseModel.Token = handler.WriteToken(securityToken);

                    return new OkObjectResult(responseModel);
                }
            }
            return new UnauthorizedResult();
        }

    }
}
