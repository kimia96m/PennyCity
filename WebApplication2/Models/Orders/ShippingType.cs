using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Orders
{
    public enum ShippingType:byte
    {
        [Description("تیپاکس")]
        Tipax=1,
        [Description("پست")]
        Post =2
       
        
    }
}
