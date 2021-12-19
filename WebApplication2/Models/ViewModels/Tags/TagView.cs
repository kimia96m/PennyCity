using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products;

namespace WebApplication2.Models.ViewModels.Tags
{
    public class TagView
    {
        public int id { get; set; }
        public string title { get; set; }
        public States? state { get; set; }
        public string creator { get; set; }
        public string createdate { get; set; }
        public string lastmodifier { get; set; }
        public string lastmodifydate { get; set; }
        public List<TagValueView> tagvalues { get; set; }
    }

}