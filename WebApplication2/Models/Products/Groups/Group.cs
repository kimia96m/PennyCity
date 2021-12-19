using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products.Specification;

namespace WebApplication2.Models.Products.Groups
{
    public class Group
    {
        public int id { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public Operator creator { get; set; }
        public DateTime createdatetime { get; set; }
        public Operator lastmodifier { get; set; }
        public DateTime? lastmodifydate { get; set; }
        public States? State { get; set; }
        public List<SpecificationGroups> specificationgroups { get; set; }
    }
}
