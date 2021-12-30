using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Groups;

namespace WebApplication2.Repository.EF
{
    public class GroupRepository : IGroupRepository
    {
        private ApplicationDbContext Context;
        public GroupRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }
        

        public async Task AddAsync(Group group)
        {
            await Context.group.AddAsync(group);
        }

        public async Task AddbannerAsync(BannerGroups bannergroup)
        {
            await Context.bannergroup.AddAsync(bannergroup);
        }

        public async Task DeleteAsync(Group group)
        {
            Group gr = await Context.group.FindAsync(group.id);
            Context.group.Remove(gr);
        }

        public async Task DeleteBannerAsync(BannerGroups bannergroup)
        {
            var gr = await Context.bannergroup.FindAsync(bannergroup.id);
            Context.bannergroup.Remove(gr);
        }

        public async Task<Group> FindAsync(int id)
        {
            var group = await Context.group.Include(v=>v.creator).Include(s=>s.specificationgroups).Where(b => b.id == id).ToAsyncEnumerable().SingleOrDefault();
            return group;
        }

        public async Task<BannerGroups> FindBannerAsync(int id)
        {
            var group = await Context.bannergroup.Include(v => v.creator).Where(b => b.id == id).ToAsyncEnumerable().SingleOrDefault();
            return group;
        }
        public async Task<BannerGroups> FindBannernumberAsync(int gid)
        {
            var group = await Context.bannergroup.Include(v => v.creator).Where(b => b.groupnumber == gid).ToAsyncEnumerable().SingleOrDefault();
            return group;
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> SearchAsync(string primaryTitle, int? id, States? state)
        {
            var q = await Context.group.Include(l=>l.lastmodifier).Include(c=>c.creator).Where(b => (b.title == primaryTitle || string.IsNullOrEmpty(primaryTitle)) && (b.id == id || id == null) && (b.State == state || state == null)).ToAsyncEnumerable().ToList();
            return q;
        }
        public async Task<IEnumerable<Group>> SearchAsync(string primaryTitle)
        {
            var q = await Context.group.Where(b => (b.title == primaryTitle || string.IsNullOrEmpty(primaryTitle))).ToAsyncEnumerable().ToList();
            return q;
        }
        public async Task<IEnumerable<BannerGroups>> SearchBannerAsync(string title, int? id)
        {
            return await Context.bannergroup.Where(x=> string.IsNullOrEmpty(title) && (id==null)).ToAsyncEnumerable().ToList();
        }

        public async Task Update(Group group)
        {
            var br = await Context.group.FindAsync(group.id);
            br.id = group.id;
            br.lastmodifier = group.lastmodifier;
            br.lastmodifydate = DateTime.UtcNow;
            br.slug = group.slug;
            br.State = group.State;
            br.title = group.title;
        }
        public async Task UpdateBanner(BannerGroups bannergroup)
        {
            var br = await Context.bannergroup.FindAsync(bannergroup.id);
            br.lastmodifier = bannergroup.lastmodifier;
            br.lastmodifydate = DateTime.UtcNow;
            br.title = bannergroup.title;
            br.groupnumber = bannergroup.groupnumber;
            br.id = bannergroup.id;
            br.ext = bannergroup.ext;
        }
    }
}
