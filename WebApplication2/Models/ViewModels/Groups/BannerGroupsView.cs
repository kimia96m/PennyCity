using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.ViewModels.Groups
{
    public class BannerGroupsView
    {
        public int id { get; set; }
        public int groupnumber { get; set; }
        public string title { get; set; }
        public string creator { get; set; }
        public string createdatetime { get; set; }
        public string lastmodifier { get; set; }
        public string lastmodifydate { get; set; }
        public string imgurl { get; set; }
    }
}
