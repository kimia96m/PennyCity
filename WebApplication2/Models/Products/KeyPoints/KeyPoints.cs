using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Areas.Admin.Models.KeyPoints;

namespace WebApplication2.Models.Products.KeyPoints
{
    public class KeyPoint
    {
        public string title { get; set; }      
        public int id { get; set; }
        public KeyPointsTypes types { get; set; }
        public Operator creator { get; set; }
        public DateTime creatdatetime { get; set; }
        public Operator lastmodifier { get; set; }
        public DateTime? lastmodifydate { get; set; }
        public Product product { get; set; }
    }
}
