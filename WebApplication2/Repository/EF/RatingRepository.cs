using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Products.Ratings;

namespace WebApplication2.Repository.EF
{
    public class RatingRepository : IRatingRepository
    {
        private ApplicationDbContext _context;
        public RatingRepository(ApplicationDbContext context)
        {
           _context = context;
        }
        public async Task AddAsync(Rating rating)
        {
           await _context.ratings.AddAsync(rating);
        }

        public void Delete(Rating rating)
        {
            _context.ratings.Remove(rating);
        }

        public async Task<Rating> FindProductItem(int productitemid,string customer)
        {
            var q = await _context.ratings.Where(p => (p.ProductItemId == productitemid)
            &&(p.customer==customer)).ToAsyncEnumerable().SingleOrDefault();
            return q;
        }

        public async Task<Rating> FindSeller(int sellerid)
        {
            var q = await _context.ratings.Where(p => p.SellerId == sellerid).ToAsyncEnumerable().SingleOrDefault();
            return q;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Rating rating)
        {
            var q = await _context.ratings.Where(x => x.Id == rating.Id).FirstOrDefaultAsync();
            q.Rate = rating.Rate;
            

        }
       public async Task<IEnumerable<Rating>> SearchProductItem(int productitemid)
        {
            var q = await _context.ratings.Where(p => p.ProductItemId == productitemid).ToAsyncEnumerable().ToList();
            return q;
        }
    }
}
