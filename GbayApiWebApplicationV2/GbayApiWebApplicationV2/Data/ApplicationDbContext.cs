using GbayApiWebApplicationV2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GbayApiWebApplicationV2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<ApplicationUser> user { get; set; }
        public DbSet<Product> Products { get; set; }
    }
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Products.Any())
                {
                    return;
                }

                context.Products.AddRange(

                    new Product
                    {
                        ProductName = "Test Product",
                        Description = "This is a great test product to test ths web application",
                        Price = 20.00m,
                        Seller = "Administrator",
                        ImgUrl = "https://www.moltonbrown.sa/wp-content/uploads/sites/4/2018/06/test-product-not-for-sale-1.jpg"
                    },
                    new Product
                    {
                        ProductName = "Test Product 2",
                        Description = "This is a great test product to test ths web application",
                        Price = 6.00m,
                        Seller = "Seller",
                        ImgUrl = "https://cdn.shopify.com/s/files/1/0532/2477/products/test-product.jpg?v=1432753385"
                    },
                    new Product
                    {
                        ProductName = "Test Product 3",
                        Description = "This is a great test product to test ths web application",
                        Price = 12.00m,
                        Seller = "Administrator",
                        ImgUrl = "https://www.zenzonegym.com/wp-content/uploads/2019/04/test-product.jpg"
                    },
                    new Product
                    {
                        ProductName = "Test Product 4",
                        Description = "This is a great test product to test ths web application",
                        Price = 100.00m,
                        Seller = "Grant",
                        ImgUrl = "https://pfwo.com/image/cache/catalog/porto/index18/tstprod-500x500.jpg"
                    });
                context.SaveChanges();

            }
        }
    }
}
