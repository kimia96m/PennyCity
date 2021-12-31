using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products.Tags;

namespace WebApplication2.Repository.EF
{
    public class TagRepository : ITagRepository
    {
        private ApplicationDbContext context;
        public TagRepository(ApplicationDbContext context) 
        {
            this.context = context;
        }

        public async Task Add(Tag tags)
        {
            await context.AddAsync(tags);   
        }

        public async Task Delete(int id)
        {
            var tag = await context.tags.FindAsync(id);
             context.tags.Remove(tag);
        }

        public async Task<Tag> Find(int id)
        {
            return await context.tags.Include(t => t.creator).Include(t => t.lastmodifier).Include(t=>t.tagValues).FirstOrDefaultAsync(t => t.id == id);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> Search(int? id, string title)
        {
            return await context.tags.Where(p => (p.id == id || id == null)
            && (p.title == title || string.IsNullOrEmpty(title)))
          .Include(o => o.creator).Include(l => l.lastmodifier).Include(x => x.tagValues).ToAsyncEnumerable().ToList();
        }
        public async Task<IEnumerable<Tag>> Search(string title)
        {
            return await context.tags.Where(p => (p.title == title || string.IsNullOrEmpty(title)))
          .Include(o => o.creator).Include(l => l.lastmodifier).Include(x => x.tagValues).ToAsyncEnumerable().ToList();
        }

        public async Task Update(Tag tags)
        {
            var tagz= await context.tags.Include(t => t.creator).Include(t => t.lastmodifier).FirstOrDefaultAsync(t => t.id ==tags.id);
            tagz.id = tags.id;
            tagz.lastmodifier = tags.lastmodifier;
            tagz.lastmodifydate = tags.lastmodifydate;
            tagz.state = tags.state;
            tagz.title = tags.title;
             context.tags.Update(tagz);
            context.Entry(tagz).Property(p => p.createdate).IsModified = false;
            context.Entry(tagz).Reference(p => p.creator).IsModified = false;
        }
    }
}
