using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products.Specification;

namespace WebApplication2.Repository.EF
{
    public class SpecificationRepository : ISpecificationRepository
    {
        public ApplicationDbContext context;
        public SpecificationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Specification specification)
        {
            await context.specifications.AddAsync(specification);
        }

        public async Task DeleteAsync(int id)
        {
            var specifications = await context.specifications.FindAsync(id);
            context.Remove(specifications);
        }

        public async Task<Specification> FindAsync(int id)
        {
            var specificatoins = await context.specifications.Include(s => s.specificationgroups).Include(s=>s.specificationvalues).FirstOrDefaultAsync(s=>s.id==id);
                return specificatoins;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Specification>> SearchAsync(string title, int specificationgid)
        {
            var specifications = await context.specifications.Where(s => s.title == title||string.IsNullOrEmpty(title) && s.specificationgroups.id == specificationgid).Include(s => s.specificationgroups).Include(s=>s.creator).Include(s=>s.lastmodifier).ToAsyncEnumerable().ToList();
            return specifications;
        }
        public async Task<IEnumerable<Specification>> SearchAsync(string title)
        {
            var specifications = await context.specifications.Where(s => s.title == title||string.IsNullOrEmpty(title)).Include(s => s.specificationgroups).ToAsyncEnumerable().ToList();
            return specifications;
        }

        public async Task UpdateAsync(Specification specification)
        {
            var specifications= await context.specifications.FindAsync(specification.id);
            specifications.title = specification.title;
            specifications.lastmodifier = specification.lastmodifier;
            specification.lastmodifydate = DateTime.UtcNow;
            specifications.state = specification.state;



            context.specifications.Update(specifications);
            context.Entry(specifications).Reference(l => l.creator).IsModified = false;
            context.Entry(specifications).Reference(l => l.specificationgroups).IsModified = false;

        }
    }
}
