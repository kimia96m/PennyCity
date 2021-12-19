using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Areas.Admin.Models.KeyPoints
{
    public enum KeyPointTypes:byte
    {
        [Description("مثبت")]
         positive=1,
        [Description("منفی")]
        negative =2
        
    }
}
