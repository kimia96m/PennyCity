using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.ViewModels.ProductItems;
using WebApplication2.Models.ViewModels.Ratings;

namespace WebApplication2.Models.ViewModels.Sellers
{
    public class SellerView
    {
        public int id { get; set; }
        public string title { get; set; }
        public RatingView rating { get; set; }
        public List<ProductItemView> sold { get; set; }
        public string paid { get; set; }
        public string creator { get; set; }
        public string createdate { get; set; }
        public string lastmodifier { get; set; }
        public string lastmodifydate { get; set; }
        public string description { get; set; }
    }
}
