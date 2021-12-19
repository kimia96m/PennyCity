using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Specification
{
    public class SpecificationValues
    { public int id { get; set; }
        public string valuetitle { get; set; }
        public States state { get; set; }
        public Product product
        {
            get;set;
        }
        public Specification specification { get; set; }
        public Operator creator { get; set; }
        public DateTime createdate { get; set; }
        public Operator lastmodifier { get; set; }
        public DateTime? lastmodifydate { get; set; }
    }
}



