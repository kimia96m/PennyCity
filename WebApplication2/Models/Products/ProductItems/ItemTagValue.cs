using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Products.Tags;

namespace WebApplication2.Models.Products.ProductItems
{
    public class ItemTagValue
    {
        public int tagvalueid { get; set; }
        public int productitemid { get; set; }
        public TagValeus tagvalues { get; set; }
        public ProductItem productitem { get; set; }
    }
}
