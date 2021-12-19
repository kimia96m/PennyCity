using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Comments
{
    public class Comment
    { public int id { get; set; }
        public string text { get; set; }
        public DateTime createdate { get; set; }
        public Operator user { get; set; }
        public int productid { get; set; }
    }
}
