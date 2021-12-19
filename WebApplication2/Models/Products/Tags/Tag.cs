using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Tags
{
    public class Tag
    {
        public int id { get; set; }
        public string title { get; set; }
        public States? state { get; set; }
        public Operator creator { get; set; }
        public DateTime createdate { get; set; }
        public Operator lastmodifier { get; set; }
        public DateTime? lastmodifydate { get; set; }
        public List<TagValeus> tagValues { get; set; }
    }
}
