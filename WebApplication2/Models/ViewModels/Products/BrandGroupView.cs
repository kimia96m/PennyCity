using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.ViewModels.Products
{
    public class BrandGroupView
    {
        public SelectList Brands { get; set; }
        public SelectList Groups { get; set; }
        public WebApplication2.Models.Products.Product Products { get; set; }
    }
}
