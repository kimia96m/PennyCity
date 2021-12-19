using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Products.Ratings;

namespace WebApplication2.Models.Sellers
{
    public class Seller
    {
        public int id { get; set; }
        public string title { get; set; }
        public Rating rating { get; set; }
        public string description { get; set; }
        public List<ProductItem> sold { get; set; }
        public double? paid { get; set; }
        public Operator creator { get; set; }
        public DateTime createdate { get; set; }
        public Operator lastmodifier { get; set; }
        public DateTime? lastmodifydate { get; set; }
    }
}
