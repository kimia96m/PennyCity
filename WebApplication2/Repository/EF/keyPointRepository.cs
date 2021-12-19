using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Areas.Admin.Models.KeyPoints;
using WebApplication2.Models;
using WebApplication2.Models.Products.KeyPoints;

namespace WebApplication2.Repository.EF
{
    public class keyPointRepository: IKeyPointRepository
    {
        private ApplicationDbContext Context;
        public keyPointRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }

        public async Task Addasync(KeyPoint keypoints)
        {
            await Context.keypoint.AddAsync(keypoints);
        }

        public void DeleteAsync(KeyPoint keyPoints)
        {
            Context.keypoint.Remove(keyPoints);
        }

        public async Task<KeyPoint> FindAsync(int id)
        {
            return await Context.keypoint.Include(k=>k.product).FirstOrDefaultAsync(k=>k.id==id);
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<KeyPoint>> SearchAsync(string title, int? id, KeyPointsTypes type, int productid)
        {
            if(type== KeyPointsTypes.positive)
            {
                return  await Context.keypoint.Where(p => (p.id == id || id == null)
                && (p.title == title || string.IsNullOrEmpty(title)
                && (p.types == KeyPointsTypes.positive)
                &&(p.product.Id==productid)
              )).Include(b => b.product).Include(o => o.creator).Include(l => l.lastmodifier).ToAsyncEnumerable().ToList();
              
            }
            else

            {
                return await Context.keypoint.Where(p => (p.id == id || id == null)
              && (p.title == title || string.IsNullOrEmpty(title)
              && (p.types == KeyPointsTypes.negative) && (p.product.Id == productid)))        
            .Include(b => b.product).Include(o => o.creator).Include(l => l.lastmodifier).ToAsyncEnumerable().ToList();
                

                //{ با خط زیر یه بار اضافه میکرد دوباره که اد میزدم از تو صفحه لیست ارور میداد  ارورای مختلف
                //    var query = await Context.keypoint.Include(b => b.product).Include(o => o.creator).Include(l => l.lastmodifier).ToAsyncEnumerable().ToList();
                //    var kplist = query
                //        .Where(p => (p.id == id || id == null)
                //        && (p.title == title || string.IsNullOrEmpty(title)
                //        && (p.types == KeyPointsTypes.negative)
                //      )).ToList();
                //    return kplist;
                //}
            }
        }

        public async Task UpdateAsync(KeyPoint keyPoints)
        {
            var keypoin = await Context.keypoint.Include(k => k.product).FirstOrDefaultAsync(k => k.id == keyPoints.id );
            keypoin.id = keyPoints.id;
            keypoin.title = keyPoints.title;
            keypoin.lastmodifier = keyPoints.lastmodifier;
            keypoin.lastmodifydate = keyPoints.lastmodifydate;
            Context.keypoint.Update(keypoin);
            Context.Entry(keyPoints).Property(p => p.creatdatetime).IsModified = false;
            Context.Entry(keyPoints).Property(p => p.types).IsModified = false;
            Context.Entry(keyPoints).Reference(p => p.creator).IsModified = false;
            Context.Entry(keyPoints).Reference(p => p.product).IsModified = false;
        }
    }
}
