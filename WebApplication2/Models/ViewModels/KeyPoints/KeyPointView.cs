using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Areas.Admin.Models.KeyPoints;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.KeyPoints;

namespace WebApplication2.Models.ViewModels.KeyPoints
{
    public class KeyPointView:KeyPointBaseView
    {
        public KeyPointsTypes type { get; set; }
        public WebApplication2.Models.Products.Product product { get; set; }
        public string creator { get; set; }
        public string createDate { get; set; }
        public string lastModifier { get; set; }
        public string lastModifyDate { get; set; }
        public States? state { get; set; }
    }
}
