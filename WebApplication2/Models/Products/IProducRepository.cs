using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products
{
   public interface IProducRepository
    {
        Task AddAsync(Product product);
        void UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<IEnumerable<Product>> SearchAsync( int? id, string primaryTitle);
        Task<Product> FindAsync(int id);
        Task SaveAsync();
        Task<IEnumerable<Product>> SearchAsync(string keyword, int? fromprice, int? toprice, int? brands, int[] specs);
        Task<IEnumerable<Product>> SearchAsync(int gid);
        Task<Product> DetailProduct(int id);
        Task<IEnumerable<Product>> FindbrAsync(int brid);


    }
}
