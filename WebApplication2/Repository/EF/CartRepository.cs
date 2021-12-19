using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products.Carts;

namespace WebApplication2.Repository.EF
{
    public class CartRepository : ICartRepository
    {
        private ApplicationDbContext context;
        public CartRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task Add(Cart cart)
        {
            await context.carts.AddAsync(cart);
        }

        public async Task Add(CartItem cartitem)
        {
            await context.cartitem.AddAsync(cartitem);
        }

        public async Task delete(int cartitemid)
        {
            var cartitem =await context.cartitem.FindAsync(cartitemid);
            context.cartitem.Remove(cartitem);
        }

        public async Task DeleteCart(int cartid)
        {
            var cart = await context.carts.FindAsync(cartid);
            context.carts.Remove(cart);
        }

        public void Deleteitems(List<CartItem> items)
        {
            context.cartitem.RemoveRange(items);

        }

        public async Task<Cart> Find(string customerid)
        {
            var cart = await context.carts.Include(c => c.cartitem).ThenInclude(x=>x.productitem).ThenInclude(c=>c.product).Include(x => x.customer).SingleOrDefaultAsync(z => z.customer.Id == customerid);
                return cart;
        }
       
        public async Task<Cart> Find(int cartid)
        {
            var cart = await context.carts.FindAsync(cartid);
                return cart;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<Cart>> Search(string customerid)
        {
            var carts = await context.carts.Where(c => c.customer.Id == customerid).Include(c => c.customer).ToAsyncEnumerable().ToList();
            return carts;
        }

        public async Task Update(CartItem cartitem)
        {
            var cartitems = await context.cartitem.FindAsync(cartitem.id);
            var count = cartitem.quantity + cartitems.quantity;
            cartitems.id = cartitem.id;
            cartitems.quantity = count;

             context.Entry(cartitem).Reference(c => c.cart).IsModified = false;
             context.Entry(cartitem).Reference(c => c.productitem).IsModified = false;
        }
    }
}
