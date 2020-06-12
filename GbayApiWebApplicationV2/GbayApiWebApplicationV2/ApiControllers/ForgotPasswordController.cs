using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GbayApiWebApplicationV2.Models;
using GbayApiWebApplicationV2.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GbayApiWebApplicationV2.ApiControllers
{
    [Route("api/[controller]")]
    public class ForgotPasswordController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public ForgotPasswordController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }


        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]ForgotPasswordViewModel model)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(model.email);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var confirmationLink = Url.Action("index", "Home",
                                       new { UserId = user.Id, Token = token }, Request.Scheme);
                confirmationLink += "#ResetPasswordModal";

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(configuration["EmailUsernameSecret"], configuration["EmailPasswordSecret"]);

                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress("Grant Shanklin", "grantshanklintest@gmail.com");
                message.From.Add(from);
                MailboxAddress to = new MailboxAddress(user.UserName, user.Email);
                message.To.Add(to);
                message.Subject = "Forgot Password";
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = $"To reset your password proceed to this link and follow the instructions on screen. Link: {confirmationLink}";
                message.Body = bodyBuilder.ToMessageBody();

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }

            return new OkResult();
        }
    }
}
