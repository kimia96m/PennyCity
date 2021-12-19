using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Orders
{
    public enum OrderState:byte
    {
        [Description("پرداخت شده")]
        paid = 1,
        [Description("پرداخت نشده")]
        unpaid = 2,
        [Description("درحال بررسی")]
        reviewing = 3
    }
}
