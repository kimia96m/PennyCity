using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products;

namespace WebApplication2.Models.ViewModels.Brands
{
    public class BrandView
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string creator { get; set; }
        public string createdatetime { get; set; }
        public string lastmodifier { get; set; }
        public string lastmodifydate { get; set; }

        public States? State { get; set; }
    }
}
