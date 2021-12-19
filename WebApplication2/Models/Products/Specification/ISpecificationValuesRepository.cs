using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Specification
{
   public interface ISpecificationValuesRepository
    {
        Task AddAsync(List<SpecificationValues> specificationvalues);
        Task UpdateAsync(SpecificationValues specificationvalues);
        Task DeleteAsync(int id);
        Task<SpecificationValues> FindAsync(int id);
        Task<IEnumerable<SpecificationValues>> SearchAsync(string title);
        Task SaveAsync();
    }
}
