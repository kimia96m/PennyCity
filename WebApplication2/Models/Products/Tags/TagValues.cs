using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Products.ProductItems;

namespace WebApplication2.Models.Products.Tags
{
    public class TagValeus
    {
        public int id { get; set; }
        public string title { get; set; }
        public States? state { get; set; }
        public Operator creator { get; set; }
        public DateTime createdate { get; set; }
        public Operator lastmodifier { get; set; }
        public DateTime? lastmodifydate { get; set; }
        public Tag tags { get; set; }
        public IEnumerable<ItemTagValue> itemtagvalue { get; set; }
    }
}
