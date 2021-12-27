using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Repository.EF
{
    public class ProductRepository : IProducRepository
    {
        private ApplicationDbContext Context;
        public ProductRepository(ApplicationDbContext context)
        {
            this.Context = context;
            
        }
        public async Task AddAsync(Product product)
        {
            await Context.product.AddAsync(product);
        }

        public async Task DeleteAsync(Product product)
        {
            var pr = await Context.product.FindAsync(product.Id);
            Context.product.Remove(pr);
        }

      
      
        public async Task<Product> FindAsync(int id)
        {
            var product = await Context.product.Include(r=>r.ratings).Include(p => p.Groups).Include(p => p.Brands).Include(p=>p.productitem).Include(p=>p.specificationvalues).ThenInclude(s=>s.specification).ThenInclude(s=>s.specificationgroups).Include(p=>p.Keypoints).Where(p => p.Id == id).ToAsyncEnumerable().SingleOrDefault();
            return product;
        }
        public async Task<IEnumerable<Product>> FindbrAsync(int brid)
        {
            var product = await Context.product.Include(p => p.Brands).Where(p => p.Brandid == brid).ToAsyncEnumerable().ToList();
            return product;
        }
        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        //public async Task<IEnumerable<Product>> SearchAsync(int? id, string PrimaryTitle)
        //{
        //    var query = await Context.Products.Include(b => b.brand).ToAsyncEnumerable().ToList();
        //    return query;
        //}

    

    
        public async Task<IEnumerable<Product>> SearchAsync(int? id, string primaryTitle)
        {
            var query = await Context.product.Include(b => b.Brands).Include(o => o.Creator).Include(o => o.Groups).Include(l => l.LastModifier).ToAsyncEnumerable().ToList();
            var productlist = query.Where(p => (p.Id == id || id == null) && (p.PrimaryTitle == primaryTitle || string.IsNullOrEmpty(primaryTitle))).ToList();
            return productlist;
        }
        public async Task<IEnumerable<Product>> SearchAsync(int gid)
        {
            var query = await Context.product.Include(b => b.Brands).Include(c => c.Groups).ThenInclude(s => s.specificationgroups).Include(i => i.productitem).ToAsyncEnumerable().ToList();
            var productlist = query.Where(p => p.Groupid == gid).ToList();
            return productlist;
        }

        public async Task<IEnumerable<Product>> SearchAsync(string keyword,int? fromprice, int? toprice, int? brands, int[] specs)
        {
            var query = await Context.product.Include(b => b.Brands).Include(c => c.Groups).Include(i => i.productitem).Include(s=>s.specificationvalues).ThenInclude(v=>v.specification).ThenInclude(m=>m.specificationgroups)
             .Where(p => 
             (( p.PrimaryTitle.Contains(keyword) || p.SecondaryTitle.Contains(keyword) || p.Brands.title.Contains(keyword) || p.Groups.title.Contains(keyword))
             &&(p.productitem.Any(a=> (fromprice==null || a.price >=fromprice)&&(toprice==null||a.price<=toprice)))
             &&(brands==null||p.Brands.id==brands)
             && (specs.Length == 0 || p.specificationvalues.Any(s => specs.Contains(s.specification.id))) )
             ).ToAsyncEnumerable().ToList();
            return query;
        }
        public async Task<Product> DetailProduct(int id)
        {
            var query = await Context.product.Include(r=>r.ratings).Include(x => x.Brands).Include(x => x.Groups).ThenInclude(x => x.specificationgroups).ThenInclude(x => x.specification).ThenInclude(x => x.specificationvalues).Include(x => x.productitem).ThenInclude(x => x.itemtagvalue).ThenInclude(x => x.tagvalues).ThenInclude(x => x.tags).Include(x => x.Keypoints).Include(x => x.comments).ThenInclude(x=>x.user).SingleOrDefaultAsync(p=>p.Id==id);
            return query;
                
        }
        public void UpdateAsync(Product product)
        {
            Context.product.Update(product);
            Context.Entry(product).Reference(p => p.Creator).IsModified = false;
            Context.Entry(product).Property(p => p.CreatDate).IsModified = false;

        }
    }
}
