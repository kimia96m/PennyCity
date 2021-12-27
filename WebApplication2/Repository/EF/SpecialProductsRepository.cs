using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products.SpecialProduct;

namespace WebApplication2.Repository.EF
{
    public class SpecialProductsRepository : ISpecialProductsRepository
    {
        private ApplicationDbContext context;
        public SpecialProductsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task Add(SpecialProducts specials)
        {
            await context.specialprodcut.AddAsync(specials);
        }

        public async Task Delete(SpecialProducts specials)
        {
            var special = await context.specialprodcut.FindAsync(specials.id);
            context.specialprodcut.Remove(special);
        }

        public async Task<SpecialProducts> Find(int id)
        {
            var special = await context.specialprodcut.Where(s => s.pnumb == id)
                .Include(b=>b.brand)
                .ToAsyncEnumerable().SingleOrDefault();
            return special;
        }

        public async Task<SpecialProducts> FindbyTitle(string title)
        {
            var special = await context.specialprodcut.Where(s => s.title==title)
              .Include(b => b.brand)
              .ToAsyncEnumerable().SingleOrDefault();
            return special;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SpecialProducts>> Search(int? id)
        {
            var query = await context.specialprodcut.Where(c => c.id == id||id==null)
                 .Include(b => b.brand).ToAsyncEnumerable().ToList();
            return query;
        }

      
       public void Update(SpecialProducts specials)
        {
            context.specialprodcut.UpdateRange(specials);
            context.Entry(specials).Reference(p => p.brand).IsModified = false;
            //context.Entry(specials).Property(p => p.id).IsModified = false;
            context.Entry(specials).Property(p => p.pnumb).IsModified = false;
            context.Entry(specials).Property(p => p.price).IsModified = false;
            context.Entry(specials).Property(p => p.state).IsModified = false;
            context.Entry(specials).Property(p => p.title).IsModified = false;
        }
    }
}
