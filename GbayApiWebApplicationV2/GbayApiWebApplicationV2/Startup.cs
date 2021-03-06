using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using GbayApiWebApplicationV2.Data;
using GbayApiWebApplicationV2.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace GbayApiWebApplicationV2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "GbayWebApplication"));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 3;
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAntiforgery(options => { options.Cookie.Expiration = TimeSpan.Zero; });

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireSignedTokens = false,
                    ValidateActor = false,
                    SignatureValidator = delegate (string token, TokenValidationParameters validationParameters) { return new JwtSecurityToken(token); }
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAntiforgery antiforgery)
        {
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            IdentityRole adminRole = new IdentityRole
            {
                Name = "Administrators"
            };
            await roleManager.CreateAsync(adminRole);

            IdentityRole buyerRole = new IdentityRole
            {
                Name = "Buyers"
            };
            await roleManager.CreateAsync(buyerRole);

            IdentityRole sellerRole = new IdentityRole
            {
                Name = "Sellers"
            };
            await roleManager.CreateAsync(sellerRole);

            IdentityRole moderatorRole = new IdentityRole
            {
                Name = "Moderators"
            };
            await roleManager.CreateAsync(moderatorRole);

            ApplicationUser admin = new ApplicationUser
            {
                UserName = "Administrator",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                SecurityQuestion1 = "a",
                SecurityQuestion2 = "a"
            };
            await userManager.CreateAsync(admin, "P@ssword1");
            await userManager.AddToRoleAsync(admin, "Administrators");

            ApplicationUser buyerUser = new ApplicationUser()
            {
                UserName = "Buyer",
                Email = "buyer@buyer.com",
                SecurityQuestion1 = "b",
                SecurityQuestion2 = "b",
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(buyerUser, "P@ssword1");
            await userManager.AddToRoleAsync(buyerUser, buyerRole.Name);

            ApplicationUser sellerUser = new ApplicationUser()
            {
                UserName = "Seller",
                Email = "seller@seller.com",
                SecurityQuestion1 = "s",
                SecurityQuestion2 = "s",
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(sellerUser, "P@ssword1");
            await userManager.AddToRoleAsync(sellerUser, sellerRole.Name);

            ApplicationUser moderatorUser = new ApplicationUser()
            {
                UserName = "Moderator",
                Email = "moderator@moderator.com",
                SecurityQuestion1 = "m",
                SecurityQuestion2 = "m",
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(moderatorUser, "P@ssword1");
            await userManager.AddToRoleAsync(moderatorUser, moderatorRole.Name);
        }
    }
}
