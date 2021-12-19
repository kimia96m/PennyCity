using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Areas.Admin.Models.KeyPoints;

namespace WebApplication2.Models.Products.KeyPoints
{
    public interface IKeyPointRepository
    {
        Task Addasync(KeyPoint keypoints);
        Task UpdateAsync(KeyPoint keyPoints);
        Task<KeyPoint> FindAsync(int id);
        Task<IEnumerable<KeyPoint>> SearchAsync(string title, int? id, KeyPointsTypes type, int productid);
        Task SaveAsync();
        void DeleteAsync(KeyPoint keyPoints);
    }
}


