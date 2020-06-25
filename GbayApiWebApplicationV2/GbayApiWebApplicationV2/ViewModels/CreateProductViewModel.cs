using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GbayApiWebApplicationV2.ViewModels
{
    public class CreateProductViewModel
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImgUrl { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
