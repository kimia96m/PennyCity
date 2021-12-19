using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Brands
{
    public class Brand
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public Operator creator { get; set; }
        public DateTime createdatetime { get; set; }
        public Operator lastmodifier { get; set; }
        public DateTime? lastmodifydate { get; set; }
        
          public States? State { get; set; }
    }
}
