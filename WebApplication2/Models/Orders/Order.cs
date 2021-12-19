using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Orders
{
    public class Order
    {
        public int id { get; set; }
        public Operator customer { get; set; }
        public DateTime orderdate { get; set; }
        public double totalprice { get; set; }
        public ShippingType shippingtypes { get; set; }
        public PaymentType paymenttypes { get; set; }
        public List<OrderItem> orderitems { get; set; }
        public OrderState state { get; set; }
        public DateTime? registrationdate { get; set; }
        public string fishnumber { get; set; }
        public string address { get; set; }
    }
}

