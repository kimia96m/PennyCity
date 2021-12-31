using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.Products.Groups
{
   public  interface IGroupRepository
    {
        Task AddAsync(Group group);
        Task Update(Group group);
        Task DeleteAsync(Group group);
        Task<IEnumerable<Group>> SearchAsync(string primaryTitle, int? id, States? state);
        Task<Group> FindAsync(int id);
        Task SaveAsync();
        Task AddbannerAsync(BannerGroups bannergroup);
        Task DeleteBannerAsync(BannerGroups bannergroup);
        Task<IEnumerable<BannerGroups>> SearchBannerAsync(string title, int? id);
        Task<IEnumerable<Group>> SearchAdvancedAsync(string primaryTitle, int? id, States? state);
        Task<BannerGroups> FindBannerAsync(int id);
        Task UpdateBanner(BannerGroups bannergroup);
        Task<BannerGroups> FindBannernumberAsync(int gid);
        Task<IEnumerable<Group>> SearchAsync(string primaryTitle);
    }
}
