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
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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

            ApplicationUser partial = new ApplicationUser
            {
                UserName = "Grants235",
                Email = "grantshanklin@gmail.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(partial, "P@ssword1");
        }
    }
}
