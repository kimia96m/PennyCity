using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Specification;
using WebApplication2.Models.ViewModels.Groups;

namespace WebApplication2.Models.ViewModels.Specifications
{
    public class SpecificationGroupsView
    {
        public int id { get; set; }
        public string title
        {
            get; set;
        }
        public GroupView group { get; set; }
        public States state
        {
            get; set;
        }
        public List<SpecificationView> specification { get; set; }
        public string creator { get; set; }
        public string createdate { get; set; }
        public string lastmodifier { get; set; }
        public string lastmodifydate { get; set; }
    }
}
