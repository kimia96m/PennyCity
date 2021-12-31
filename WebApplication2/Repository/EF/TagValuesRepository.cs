using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Tags;

namespace WebApplication2.Repository.EF
{
    public class TagValuesRepository : ITagValuesRepository
    {
        private ApplicationDbContext context;
        public TagValuesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(TagValeus tags)
        {
            await context.AddAsync(tags);
        }

        public async Task Delete(int id)
        {
            var tag = await context.tagvalues.FindAsync(id);
            context.tagvalues.Remove(tag);
        }

        public async Task<TagValeus> Find(int id)
        {
            return await context.tagvalues.Include(t => t.tags).Include(t => t.creator).Include(t => t.lastmodifier).FirstOrDefaultAsync(t => t.id == id);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagValeus>> Search(int? id, string title, int tagid)
        {
            return await context.tagvalues.Where(p => (p.tags.id == tagid) && (p.id == id || id == null)
            && (p.title == title || string.IsNullOrEmpty(title)))
          .Include(t => t.tags).Include(o => o.creator).Include(l => l.lastmodifier).ToAsyncEnumerable().ToList();
        }
        public async Task<IEnumerable<TagValeus>> SearchAdvance(States state, string title)
        {
            return await context.tagvalues.Where(p => (p.state==state)
            && (p.title.Contains(title) ) || (title == null && p.state == state))
          .Include(t => t.tags).Include(o => o.creator).Include(l => l.lastmodifier).ToAsyncEnumerable().ToList();
        }
        public async Task<IEnumerable<TagValeus>> Search( string title)
        {
            return await context.tagvalues.Where(p => (p.title == title || string.IsNullOrEmpty(title)))
          .Include(t => t.tags).ToAsyncEnumerable().ToList();
        }

        public async Task Update(TagValeus tags)
        {
            var tagvz = await context.tagvalues.Include(t => t.tags).Include(t => t.creator).FirstOrDefaultAsync(t => t.id == tags.id);
            tagvz.id = tags.id;
            tagvz.lastmodifier = tags.lastmodifier;
            tagvz.lastmodifydate = tags.lastmodifydate;
            tagvz.state = tags.state;
            tagvz.title = tags.title;
            context.tagvalues.Update(tagvz);
            context.Entry(tagvz).Property(p => p.createdate).IsModified = false;
            context.Entry(tagvz).Reference(p => p.creator).IsModified = false;
            context.Entry(tagvz).Reference(p => p.tags).IsModified = false;

        }
    }
}
