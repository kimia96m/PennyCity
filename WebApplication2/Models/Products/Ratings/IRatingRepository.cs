using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Ratings
{
   public interface IRatingRepository
    {
        Task AddAsync(Rating rating);
        void Delete(Rating rating);
        Task<Rating> FindSeller(int sellerid);
        Task<Rating> FindProductItem(int productitemid,string customer);
        Task<IEnumerable<Rating>> SearchProductItem(int productitemid);
        Task Save();
        Task Update(Rating rating);
    }
}
