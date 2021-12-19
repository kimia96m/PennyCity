using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products.ProductItems;

namespace WebApplication2.Models.Products.Carts
{
    public class CartItem
    {
        public int id { get; set; }
        public Cart cart { get; set; }
        public ProductItem productitem { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
    }
}
