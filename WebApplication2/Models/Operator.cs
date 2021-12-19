using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication2.Models.Profiles;

namespace WebApplication2.Models
{

    public class Operator : IdentityUser
    {
     public string name { get; set; }
     public string lastname { get; set; }
     public List<Address> address { get; set; }

    }
}
