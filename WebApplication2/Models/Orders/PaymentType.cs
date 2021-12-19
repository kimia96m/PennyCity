using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Orders
{
    public enum PaymentType:byte
    {
        [Description("کارت به کارت")]
        carttocart=1,
        [Description("پرداخت آنلاین")]
        online=2,
        [Description("واریز")]
        variz=3
    }
}
