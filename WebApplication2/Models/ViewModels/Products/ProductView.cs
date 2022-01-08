using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Brands;
using WebApplication2.Models.Products.Groups;

namespace WebApplication2.Models.ViewModels.Products
{
    public class ProductView
    {
        public int Id { get; set; }
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set; }
        public string Description { get; set; }
        public string Ext { get; set; }
        public Brand Brands { get; set; }
        public Group Groups { get; set; }
        public States? state { get; set; }
        public string Creator { get; set; }
        public string CreatDate { get; set; }
        public string LastModifier { get; set; }
        public string LastModifyDate { get; set; }
        public string price { get; set; }
 
    }
}
