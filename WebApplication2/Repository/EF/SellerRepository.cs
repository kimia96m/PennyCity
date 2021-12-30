using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Models.Sellers;

namespace WebApplication2.Repository.EF
{
    public class SellerRepository : ISellerRepository
    {
        private ApplicationDbContext _context;
        public SellerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Seller seller)
        {
            await _context.sellers.AddAsync(seller);
        }

        public void Delete(Seller seller)
        {
             _context.sellers.Remove(seller);
        }

        public async Task<Seller> Find(int id)
        {
            var q = await _context.sellers.Where(p => p.id == id).ToAsyncEnumerable().SingleOrDefault();
            return q;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Seller>> Search(int? id, string title, string producttitle)
        {
            var q = await _context.sellers.Include(x=>x.sold)
                .Where(
                p => 
                (
                (p.id == id) ||id==null)
                && ((p.title==p.title)||string.IsNullOrEmpty(title))
                &&
                p.sold.Any(
                    x=>x.product.PrimaryTitle==producttitle||x.product.SecondaryTitle==producttitle) || string.IsNullOrEmpty(producttitle)
                ).ToAsyncEnumerable().ToList();
            return q;
        }
        public async Task<IEnumerable<Seller>> Search( string title)
        {
            var q = await _context.sellers.Include(x=>x.sold)
                .Where(
                p =>((p.title==p.title)||string.IsNullOrEmpty(title))
                ).ToAsyncEnumerable().ToList();
            return q;
        }
        public void Update(Seller seller)
        {
            var sellr= _context.sellers.Find(seller.id);
            sellr.id = seller.id;
            sellr.paid = seller.paid;
            sellr.description = seller.description;
            sellr.title = seller.title;
        }
    }
}
