using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Sellers;

namespace WebApplication2.Models.Products.ProductItems
{
    public class SellerProductItem
    {
        public int sellerid { get; set; }
        public int productitemid { get; set; }
        public Seller seller { get; set; }
    }
}
