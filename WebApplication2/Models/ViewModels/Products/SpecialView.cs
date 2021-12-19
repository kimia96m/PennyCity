using WebApplication2.Models.Products.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products;
namespace WebApplication2.Models.ViewModels.Products
{
    public class SpecialView
    {
        public string title { get; set; }
        public int id { get; set; }
        public string brand { get; set; }
        public States? state { get; set; }
        public string price { get; set; }
        public int prnumb { get; set; }
        public int productnumb { get; set; }
        public string discount { get; set; }
        public string date { get; set; }
        public string imgurl { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
