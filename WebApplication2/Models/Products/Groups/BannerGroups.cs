using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Groups
{
    public class BannerGroups
    {
        public int id { get; set; }
        public int groupnumber { get; set; }
        public string title { get; set; }
        public Operator creator { get; set; }
        public DateTime createdatetime { get; set; }
        public Operator lastmodifier { get; set; }
        public DateTime? lastmodifydate { get; set; }
        public string ext { get; set; }
    }
}
