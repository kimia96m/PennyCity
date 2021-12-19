using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.ProductItems
{
    public class ProductItem
    {
        public int id { get; set; }
        public Product product { get; set; }
        public double price { get; set; }
        public double? discount
        {
            get;set;
        }
        public byte quantity { get; set; }  
        public States? state { get; set; }
        public Operator creator { get; set; }
        public DateTime createdate { get; set; }
        public Operator lastmodifier { get; set; }
        public DateTime? lastmodifydate { get; set; }
        public List<ItemTagValue> itemtagvalue { get; set; }
        public int sellerid { get; set; }
    }
}

  