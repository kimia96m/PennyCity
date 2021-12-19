using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products;
using WebApplication2.Models.ViewModels.Sellers;
using WebApplication2.Models.ViewModels.Tags;

namespace WebApplication2.Models.ViewModels.ProductItems
{
    public class ProductItemView
    {
        public int id { get; set; }
        public WebApplication2.Models.Products.Product product { get; set; }
        //public TagValeus tagvalues { get; set; }
        public string price { get; set; }
        public string discount
        {
            get; set;
        }
        public string quantity { get; set; }

        public States? state { get; set; }
        public string creator { get; set; }
        public string createdate { get; set; }
        public string lastmodifier { get; set; }
        public string lastmodifydate { get; set; }
        public List<TagValueView> itemtagvalue { get; set; }
        public SellerView seller { get; set; }
    }
}
