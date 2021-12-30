using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Banners
{
   public interface IBannerRepository
    {
        Task Add(Banner banner);
        Task Delete(int id);
        Task<Banner> Find(int id);
        Task Save();
        Task<IEnumerable<Banner>> Search(int? id);
        Task<IEnumerable<Banner>> SearchByIsSpecial(int? id);
       
    }
}
