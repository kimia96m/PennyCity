using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Specification
{
    public interface ISpecificationRepository
    {
        Task AddAsync(Specification specification);
        Task UpdateAsync(Specification specification);
        Task DeleteAsync(int id);
        Task<Specification> FindAsync(int id);
        Task<IEnumerable<Specification>> SearchAsync(string title, int specificationgid);
        Task<IEnumerable<Specification>> SearchAsync(string title);
        Task SaveAsync();

    }
}
