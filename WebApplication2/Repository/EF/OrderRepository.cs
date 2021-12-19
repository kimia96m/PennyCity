using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.InfraStructure;
using WebApplication2.Models;
using WebApplication2.Models.Orders;

namespace WebApplication2.Repository.EF
{
    public class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;

        }
        public async Task Add(Order order)
        {
           await context.orders.AddAsync(order);
        }

        public async Task Add(List<OrderItem> orderitems)
        {
            await context.orderitems.AddRangeAsync(orderitems);
        }

        public async Task<Order> Find(int orderid)
        {
            return await context.orders.Include(c => c.customer).ThenInclude(x=>x.address).Include(o => o.orderitems).ThenInclude(c=>c.productitem).ThenInclude(p=>p.product).FirstOrDefaultAsync(o=>o.id==orderid);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public Task Search()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> SearchbByCustomerId(string id)
        {
            var q = await context.orders.Include(x => x.customer).Include(x => x.orderitems).ThenInclude(x => x.productitem).ThenInclude(x => x.product).Where(x => x.customer.Id== id).ToAsyncEnumerable().ToList();
            return q;
        }

        public async Task<IEnumerable<Order>> SearchbById(int? id)
        {
            var q = await context.orders.Include(x => x.customer).Include(x => x.orderitems).ThenInclude(x => x.productitem).Where(x => (x.id == id) || (id == null)).ToAsyncEnumerable().ToList();
            return q;
        }

        public async Task<IEnumerable<Order>> SearchbByIdAndState(OrderState state)
        {
            var q = await context.orders.Include(x => x.customer).Include(x => x.orderitems).ThenInclude(x => x.productitem).ThenInclude(x=>x.product).Where(x => x.state==state).ToAsyncEnumerable().ToList();
            return q;
        }

        public async Task Update(Order order)
        {
            var or= await context.orders.FindAsync(order.id);
            or.state = OrderState.paid;
            or.id = order.id;
            or.orderdate = order.orderdate;
            or.orderitems = order.orderitems;
            or.paymenttypes = order.paymenttypes;
            or.registrationdate = order.registrationdate;
            or.shippingtypes = order.shippingtypes;
            or.totalprice = order.totalprice;
                
         }

        public async Task Update(int orderid, string serial, string registrationdate)
        {
            var order = await context.orders.FindAsync(orderid);
            order.fishnumber = serial;
            order.registrationdate = DateConverter.ToMilady(registrationdate);
            order.state = OrderState.reviewing;
            context.Entry(order).Reference(c => c.customer).IsModified = false;
        }
    }
}
