using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GbayApiWebApplicationV2.ViewModels;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GbayApiWebApplicationV2.ApiControllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ManageUsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ManageUsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var currentUser = await userManager.FindByIdAsync(User.Identity.Name);
            if (currentUser != null)
            {
                var roleCheck = await userManager.GetRolesAsync(currentUser);
                if (!roleCheck.Contains("Administrators"))
                {
                    return new UnauthorizedResult();
                }
            }
            else
            {
                return new UnauthorizedResult();
            }

            List<ApplicationUser> users = userManager.Users.ToList();
            List<ManageUsersViewModel> modelList = new List<ManageUsersViewModel>(users.Count);

            foreach (var user in users)
            {
                ManageUsersViewModel vm = new ManageUsersViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    Roles = await userManager.GetRolesAsync(user),
                    SecurityQuestion1 = user.SecurityQuestion1,
                    SecurityQuestion2 = user.SecurityQuestion2
                };
                modelList.Add(vm);
            }

            return new OkObjectResult(modelList);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
