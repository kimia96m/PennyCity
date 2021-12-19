using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Brands
{
    public interface IBrandRepository
    {
        Task AddAsync(Brand brand);
        Task Update(Brand brand);
        Task DeleteAsync(Brand brand);
        Task<IEnumerable<Brand>> SearchAsync(string primaryTitle, int? id, States? state);
        
        Task<Brand> FindAsync(int id);
        Task SaveAsync();
    }
}
