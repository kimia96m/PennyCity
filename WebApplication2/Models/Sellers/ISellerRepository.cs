using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Sellers
{
   public interface ISellerRepository
    {
        Task Add(Seller seller);
        Task Delete(Seller seller);
        Task Update(Seller seller);
        Task Save();
        Task<Seller> Find(int id);
        Task<IEnumerable<Seller>> Search(int? id, string title, string producttitle);
    }
}
