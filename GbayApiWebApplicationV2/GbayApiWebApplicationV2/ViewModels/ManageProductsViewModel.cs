using GbayApiWebApplicationV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GbayApiWebApplicationV2.ViewModels
{
    public class ManageProductsViewModel
    {
        public ManageProductsViewModel()
        {
            this.Products = new List<Product>();
        }
        public List<Product> Products { get; set; }
    }
}
