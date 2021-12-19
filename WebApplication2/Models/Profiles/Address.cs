using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Profiles
{
    public class Address
    {
        public int id { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string tel { get; set; }
        public string location { get; set; }
        //public int addressofuserid { get; set; }
        public Operator customer { get; set; }
    }
}
