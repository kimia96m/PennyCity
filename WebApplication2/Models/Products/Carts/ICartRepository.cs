using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Carts
{
   public interface ICartRepository
        
    {
        Task<Cart> Find(string customerid);
        Task Add(Cart cart);
        Task Add(CartItem cartitem);
        Task<Cart> Find(int cartid);
        Task<List<Cart>> Search(string customerid);
        Task Save();
        Task Update(CartItem cartitem);
        Task delete(int cartitemid);
        Task DeleteCart(int cartid);
        void Deleteitems(List<CartItem> items);
    }
}



