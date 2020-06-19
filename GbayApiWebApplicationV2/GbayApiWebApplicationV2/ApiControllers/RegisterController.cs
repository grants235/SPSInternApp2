using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Models;
using GbayApiWebApplicationV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GbayApiWebApplicationV2.ApiControllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public RegisterController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]RegisterViewModel model)
        {
            if (model.Password != null || model.Username != null || model.Email != null || model.ConfirmPassword != null)
            {
                if (model.Password == model.ConfirmPassword)
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = model.Username,
                        Email = model.Email,
                    };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Buyers");
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action("ConfirmEmail", "Home",
                                               new { userId = user.Id, token = token }, Request.Scheme);

                        SmtpClient client = new SmtpClient();
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate(configuration["EmailUsernameSecret"], configuration["EmailPasswordSecret"]);

                        MimeMessage message = new MimeMessage();
                        MailboxAddress from = new MailboxAddress("Grant Shanklin", "grantshanklintest@gmail.com");
                        message.From.Add(from);
                        MailboxAddress to = new MailboxAddress(user.UserName, user.Email);
                        message.To.Add(to);
                        message.Subject = "Confirm Email";
                        BodyBuilder bodyBuilder = new BodyBuilder();
                        bodyBuilder.TextBody = $"Please confirm your email by clicking on this link: {confirmationLink}";
                        message.Body = bodyBuilder.ToMessageBody();

                        client.Send(message);
                        client.Disconnect(true);
                        client.Dispose();

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
