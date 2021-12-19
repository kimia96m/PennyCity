using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products.Brands;
using WebApplication2.Models.Products.Groups;

namespace WebApplication2.Models.ViewModels.ProductLists
{
    public class ProductListView
    {
        public int Id { get; set; }
        public string Primarytitle { get; set; }
        public string Secoundarytitle { get; set; }
        public string Price { get; set; }
        public string Imagename { get; set; }
        public Group group { get; set; }
        public Brand brand { get; set; }
    }
}
