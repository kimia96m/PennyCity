using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Banners
{
    public class Banner
    {
        public int id { get; set; }
        public string link { get; set; }
        public string imgagesrc { get; set; }
        public string ext { get; set; }
        public bool? ispecial { get; set; }
    }
}
