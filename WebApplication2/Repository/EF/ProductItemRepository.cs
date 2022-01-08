using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Products.Specification;

namespace WebApplication2.Repository.EF
{
    public class ProductItemRepository : IProductItemRepository
    {
        private ApplicationDbContext context;
        public ProductItemRepository(ApplicationDbContext context)
        {
            this.context = context;

        }
        public async Task AddAsync(ProductItem productitem)
        {
            await context.productitems.AddAsync(productitem);
        }

        public async Task AddItemTagValuesAsync(List<ItemTagValue> itemtagvalue)
        {
            await context.itemtagvalues.AddRangeAsync(itemtagvalue);
        }

        public async Task DeleteAsync(int id)
        {
            var pitem = await context.productitems.FindAsync(id);
            context.productitems.Remove(pitem);
        }

        public async Task<ProductItem> FindAsync(int id)
        {
            var pitem = await context.productitems.Include(x => x.product).ThenInclude(b=>b.Brands).Include(x => x.product).ThenInclude(x=>x.specificationvalues).ThenInclude((SpecificationValues c)=>c.specification).Include(t => t.itemtagvalue).ThenInclude((ItemTagValue i) => i.tagvalues).ThenInclude(t => t.tags).Where(x => x.id == id).FirstOrDefaultAsync();
            return pitem;
        }
        //.

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<ProductItem>> SearchAsync(int? pid)
        {
            return
                 await context.productitems.Where(x => x.product.Id == pid)
                 .Include(x => x.creator)
                 .Include(x => x.lastmodifier)
                 .Include(x => x.product)
                 .Include(x => x.itemtagvalue)
                 .ThenInclude((ItemTagValue i)=>i.tagvalues)
                 .ThenInclude(t => t.tags)
                 .ToAsyncEnumerable().ToList();
        }
        public async Task<List<ProductItem>> SearchAsync()
        {
            return
                 await context.productitems        
                 .Include(x => x.product)
                 .ThenInclude(x=>x.Brands)
                 .Include(x => x.itemtagvalue)
                 .ThenInclude((ItemTagValue i) => i.tagvalues)
                 .ThenInclude(t => t.tags)
                 .ToAsyncEnumerable().ToList();
        }

        public async Task itemtagvaluemerge(List<ItemTagValue> itemtagvalue)
        {
            using (var transaction = await context.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadUncommitted))
            {
                var productitemid = itemtagvalue.Select(t => t.productitemid).FirstOrDefault();
                var itemtagvalues = await context.itemtagvalues.Where(s => s.productitemid == productitemid).ToAsyncEnumerable().ToList();
                context.itemtagvalues.RemoveRange(itemtagvalues);
                await context.SaveChangesAsync();
                await context.itemtagvalues.AddRangeAsync(itemtagvalue);
                transaction.Commit();
            }
           
        }
      
   
        //public async Task<List<ProductItem>> SearchAsync(int? pid)
        //{
        //    return
        //         await context.productitems.Where(x => x.product.Id == pid).Include(x => x.itemtagvalue).Include(x => x.product).Include(x => x.creator).Include(x => x.lastmodifier).ToAsyncEnumerable().ToList();
        //}
        public async Task UpdateAsync(ProductItem productitem)
        {
            var pitem = await context.productitems.Include(x => x.product).Where(x => x.id == productitem.id).FirstOrDefaultAsync();
            pitem.id = productitem.id;
            pitem.lastmodifier = productitem.lastmodifier;
            pitem.lastmodifydate = DateTime.UtcNow;
            pitem.price = productitem.price;
            pitem.discount = productitem.discount;
            pitem.quantity = productitem.quantity;
            pitem.state = productitem.state;
            pitem.sellerid = productitem.sellerid;

            context.Entry(pitem).Reference(x => x.creator).IsModified = false;
            context.Entry(pitem).Reference(x => x.product).IsModified = false;
            context.Entry(pitem).Property(x => x.createdate).IsModified = false;


        }

        public async Task Update(List<ProductItem> productitems)
        {
            var items = new List<ProductItem>();
            foreach (var item in productitems)
            {
                var pitem = await context.productitems.FindAsync(item.id);
                pitem.quantity = item.quantity;
                 context.Entry(pitem).Reference(c => c.creator).IsModified = false;
                context.Entry(pitem).Reference(c => c.product).IsModified = false;
                context.Entry(pitem).Reference(c => c.lastmodifier).IsModified = false;
                context.Entry(pitem).Property(c => c.createdate).IsModified = false;
                context.Entry(pitem).Property(c => c.lastmodifydate).IsModified = false;
                items.Add(pitem);

            }
            context.productitems.UpdateRange(items);
        }
    }
}
