using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products.Specification;

namespace WebApplication2.Repository.EF
{
    public class SpecificationValuesRepository : ISpecificationValuesRepository
    {
        public ApplicationDbContext context;
        public SpecificationValuesRepository(ApplicationDbContext context)
        {
            this.context = context;
                
        }
        public async Task AddAsync(List<SpecificationValues> specificationvalues)
        {
            await context.specificationvalues.AddRangeAsync(specificationvalues);
        }

        public async Task DeleteAsync(int id)
        {
            var specivalue = await context.specificationvalues.FindAsync(id);
             context.Remove(specivalue);
        }

        public async Task<SpecificationValues> FindAsync(int id)
        {
            var specivalue = await context.specificationvalues.Include(s => s.specification).Include(p=>p.product).FirstOrDefaultAsync(s => s.id==id);
            return specivalue;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SpecificationValues>> SearchAsync(string title)
        {
            var specivalue = await context.specificationvalues.Where(s => s.valuetitle == title || string.IsNullOrEmpty(title)).Include(s => s.specification).Include(s => s.creator).Include(s => s.lastmodifier).Include(s=>s.product).ToAsyncEnumerable().ToList();
            return specivalue;
        }

        public async Task UpdateAsync(SpecificationValues specificationvalues)
        {
            var specivalue = await context.specificationvalues.FindAsync(specificationvalues.id);
            specivalue.valuetitle = specificationvalues.valuetitle;
            specivalue.state = specificationvalues.state;
            specivalue.lastmodifier = specificationvalues.lastmodifier;
            specivalue.lastmodifydate = DateTime.UtcNow;

            context.specificationvalues.Update(specivalue);
            context.Entry(specivalue).Reference(s => s.creator).IsModified = false;
            context.Entry(specivalue).Reference(s => s.specification).IsModified = false;
        }
    }
}
