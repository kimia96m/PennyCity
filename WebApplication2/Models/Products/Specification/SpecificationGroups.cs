using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products.Groups;

namespace WebApplication2.Models.Products.Specification
{
    public class SpecificationGroups
    {
        public int id { get; set; }
        public string title
        {
            get;set;
        }
        public Group group { get; set; }
        public States state
        {
            get;set;
        }
        public List<Specification> specification { get; set; }
        public Operator creator { get; set; }
        public DateTime createdate { get; set; }
        public Operator lastmodifier { get; set; }
        public DateTime? lastmodifydate { get; set; }
    }
}


