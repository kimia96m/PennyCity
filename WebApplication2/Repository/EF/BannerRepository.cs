using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Banners;

namespace WebApplication2.Repository.EF
{
    public class BannerRepository : IBannerRepository
    {
        private ApplicationDbContext _context;
        public BannerRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task Add(Banner banner)
        {
            await _context.banners.AddAsync(banner);
        }

        public async Task Delete(int id)
        {
            var query = await _context.banners.FindAsync(id);
            _context.banners.Remove(query);
        }

        public async Task<Banner> Find(int id)
        {
            var query = await _context.banners.Where(c => c.id == id).ToAsyncEnumerable().SingleOrDefault();
            return query;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Banner>> Search(int? id)
        {
            var banner = await _context.banners.Where(b=>b.id==id ||id==null).ToAsyncEnumerable().ToList();
            return banner;
        }

        public async Task<IEnumerable<Banner>> SearchByIsSpecial(int? id)
        {
            var banner = await _context.banners.Where(b => (b.id == id || id == null)&& b.ispecial==true).ToAsyncEnumerable().ToList();
            return banner;
        }
    }
}
