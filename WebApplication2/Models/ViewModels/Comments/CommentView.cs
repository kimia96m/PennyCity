using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.ViewModels.Comments
{
    public class CommentView
    {
        public int id { get; set; }
        public string text { get; set; }
        public string createdate { get; set; }
        public string user { get; set; }
        public int productid { get; set; }
    }
}
