using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Specification
{
    public interface ISpecificationGroupsRepository
    {

        Task AddAsync(SpecificationGroups specificationgroups);
        Task UpdateAsync(SpecificationGroups specificationgroups);
        Task<SpecificationGroups> FindAsync(int id);
        Task<IEnumerable<SpecificationGroups>> SearchAsync(string title, int gid);
        Task<List<SpecificationGroups>> SearchAsync(int? gid);
        Task<IEnumerable<SpecificationGroups>> SearchAsync(string title);
        Task Delete(int id);
        Task SaveAsync();

    }
}
