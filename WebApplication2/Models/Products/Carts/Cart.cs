using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Carts
{
    public class Cart
    {
        public int id { get; set; }
        public Operator customer { get; set; }
        public List<CartItem> cartitem { get; set; }
    }
}
