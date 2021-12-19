using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Orders
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task Add (List<OrderItem > orderitems);
        Task<Order> Find(int orderid);
        Task Save();
        Task Update(Order order);
        Task Update(int orderid, string serial, string registrationdate);
        Task<IEnumerable<Order>> SearchbById(int? id);
        Task<IEnumerable<Order>> SearchbByCustomerId(string id);
        Task<IEnumerable<Order>> SearchbByIdAndState(OrderState state);
        Task Search();
    }
}
