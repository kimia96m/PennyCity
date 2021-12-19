using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.ViewModels.Tags;

namespace WebApplication2.Models.ViewModels.ProductItems
{
    public class ItemTagValueView
    {
        public int tagvalueid { get; set; }
        public int productitemid { get; set; }
        public TagValueView tagvalues { get; set; }
        public ProductItemView productitem { get; set; }
    }
}
