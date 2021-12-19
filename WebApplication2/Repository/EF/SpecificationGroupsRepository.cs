using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products.Specification;

namespace WebApplication2.Repository.EF
{
    public class SpecificationGroupsRepository : ISpecificationGroupsRepository
    {
        public ApplicationDbContext context;
       
        public SpecificationGroupsRepository(ApplicationDbContext Context)
        {
            this.context = Context;
          
        }
        public async Task AddAsync(SpecificationGroups specificationgroups)
        {
            await context.specificationgroups.AddAsync(specificationgroups);
        }

        public async Task Delete(int id)
        {
            var specificationgroup = await context.specificationgroups.FindAsync(id);
            context.Remove(specificationgroup);
        }

        public async Task<SpecificationGroups> FindAsync(int id)
        {
            var specificationgroup = await context.specificationgroups.Include(s => s.group).Include(s=>s.specification).FirstOrDefaultAsync(s=>s.id==id);
            return specificationgroup;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SpecificationGroups>> SearchAsync(string title,int gid)
        {
            var specificationgroup = await context.specificationgroups.Where(s => s.title == title|| string.IsNullOrWhiteSpace(title)
            &&(s.group.id==gid)).Include(g=>g.group).Include(s=>s.creator).Include(s=>s.lastmodifier).ToAsyncEnumerable().ToList();
            return specificationgroup;

        }
        public async Task<List<SpecificationGroups>> SearchAsync(int? gid)
        {
            var specificationgroup = await context.specificationgroups.Where( x=>x.group.id==gid ).Include(g => g.group).Include(s=>s.specification).Include(s => s.creator).Include(s => s.lastmodifier).ToAsyncEnumerable().ToList();
            return specificationgroup;

        }

        public async Task UpdateAsync(SpecificationGroups specificationgroups)
        {
            var specification = await context.specificationgroups.FindAsync(specificationgroups.id);
            specification.state = specificationgroups.state;
            specification.lastmodifier = specificationgroups.lastmodifier;
            specification.lastmodifydate = specificationgroups.lastmodifydate;
            specification.title = specificationgroups.title;

            context.specificationgroups.Update(specification);
            context.Entry(specification).Reference(p => p.group).IsModified = false;
            context.Entry(specification).Reference(p => p.creator).IsModified = false;
            context.Entry(specification).Property(p => p.createdate).IsModified = false;

        }
    }

    
        
}
