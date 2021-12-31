using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Brands;

namespace WebApplication2.Repository.EF
{
    public class BrandRepository : IBrandRepository
    {
        private ApplicationDbContext Context;
        public BrandRepository(ApplicationDbContext context)
        {
            this.Context = context;
                
        }

        public async Task AddAsync(Brand brand)
        {
            await Context.Brand.AddAsync(brand);
        }

        public async Task DeleteAsync(Brand brand)
        {
            Brand br = await Context.Brand.FindAsync(brand.id);
            Context.Brand.Remove(br);
        }

        public async Task<Brand> FindAsync(int id)
        {

           
            var brand = await Context.Brand.Include(v=>v.creator).Where(b => b.id == id).ToAsyncEnumerable().SingleOrDefault();
            return brand;
         }
       
        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Brand>> SearchAsync(string primaryTitle, int? id, States? state)
        {
            var q = await Context.Brand.Include(o => o.creator).Include(p => p.lastmodifier)
            .Where(b => (b.title == primaryTitle || string.IsNullOrEmpty(primaryTitle)) && (b.id == id || id == null) && (b.State == state || state == null)).ToAsyncEnumerable().ToList();
            return q;
        }
        public async Task<IEnumerable<Brand>> SearchAdvancedAsync(string primaryTitle, int? id, States? state)
        {
            var q = await Context.Brand.Include(o => o.creator).Include(p => p.lastmodifier)
            .Where(b => ((b.title == primaryTitle)||(b.slug==primaryTitle) || (b.id == id)) && (b.State == state)).ToAsyncEnumerable().ToList();
            return q;
        }
        public async Task<IEnumerable<Brand>> SearchAsync(string primaryTitle)
        {
            var q = await Context.Brand.ToAsyncEnumerable().ToList();
            var brands = await q.Where(b => (b.title == primaryTitle || string.IsNullOrEmpty(primaryTitle))).ToAsyncEnumerable().ToList();
            return brands;
        }

        //void Update(Brand brand)
        //{
        //    //var br = await Context.Brand.FindAsync(brand.id);
        //    //br.id = brand.id;
        //    //br.lastmodifier = brand.lastmodifier;
        //    //br.lastmodifydate = brand.lastmodifydate;
        //    //br.slug = brand.slug;
        //    //br.State = brand.State;
        //    //br.title = brand.title;


        //}

        public async Task Update(Brand brand)
        {
            var br = await Context.Brand.FindAsync(brand.id);
            br.id = brand.id;
            br.lastmodifier = brand.lastmodifier;
            br.lastmodifydate = DateTime.UtcNow;
            br.slug = brand.slug;
            br.State = brand.State;
            br.title = brand.title;
            
           
           
            //Context.Brand.Update(brand);
        }

        //public Task UpdateAsync(Brand brand)
        //{
        //    var br = await Context.Brand.FindAsync(brand.id);
        //    br.id = brand.id;
        //    br.lastmodifier = brand.lastmodifier;
        //    br.lastmodifydate = brand.lastmodifydate;
        //    br.slug = brand.slug;
        //    br.State = brand.State;
        //    br.title = brand.title;
        //}
    }
}
