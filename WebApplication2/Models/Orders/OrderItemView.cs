using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.ViewModels.ProductItems;

namespace WebApplication2.Models.Orders
{
    public class OrderItemView
    {
        public long id { get; set; }
        public OrderView order { get; set; }
        public ProductItemView productitem { get; set; }
        public string price { get; set; }
        public int quantity { get; set; }
    }
}
