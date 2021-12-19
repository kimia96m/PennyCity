using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Tags;

namespace WebApplication2.Models.ViewModels.Tags
{
    public class TagValueView
    {
        public int id { get; set; }
        public string title { get; set; }
        public States? state { get; set; }
        public string creator { get; set; }
        public string createdate { get; set; }
        public string lastmodifier { get; set; }
        public string lastmodifydate { get; set; }
        public TagView tagviews { get; set; }
    }
}

