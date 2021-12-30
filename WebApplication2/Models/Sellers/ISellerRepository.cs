using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Sellers
{
   public interface ISellerRepository
    {
        Task Add(Seller seller);
        void Delete(Seller seller);
        void Update(Seller seller);
        Task Save();
        Task<Seller> Find(int id);
        Task<IEnumerable<Seller>> Search(int? id, string title, string producttitle);
        Task<IEnumerable<Seller>> Search(string title);
    }
}
