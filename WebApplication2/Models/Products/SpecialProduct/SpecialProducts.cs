using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products.Brands;
using WebApplication2.Models.Products.Specification;

namespace WebApplication2.Models.Products.SpecialProduct
{
    public class SpecialProducts
    {
        public string title { get; set; }
        public int id { get; set; }
        public int pnumb { get; set; }
        public Brand brand { get; set; }
        public States? state { get; set; }
        public double price { get; set; }
        public TimeSpan? leftedtime { get; set; }
        public String lefteddays { get; set; }
        public int? discount { get; set; }
        public List<SpecificationValues> specificationvalues { get; set; }
    }
}
