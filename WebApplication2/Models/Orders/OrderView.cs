using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Orders
{
    public class OrderView
    {
        public int id { get; set; }
        public string customer { get; set; }
        public string orderdate { get; set; }
        public string totalprice { get; set; }
        public string shippingtypes { get; set; }
        public string paymenttypes { get; set; }
        public List<OrderItemView> orderitems { get; set; }
        public string state { get; set; }
        public string registrationdate { get; set; }
        public string fishnumber { get; set; }
        public string address { get; set; }
        public string customerid { get; set; }
    }
}
