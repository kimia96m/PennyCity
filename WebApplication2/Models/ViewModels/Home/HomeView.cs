using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Banners;
using WebApplication2.Models.Products.Groups;
using WebApplication2.Models.ViewModels.Groups;
using WebApplication2.Models.ViewModels.Products;

namespace WebApplication2.Models.ViewModels.Home
{
    public class HomeView
    {
        public List<Banner> banners { get; set; }
        public List<SpecialView> specialproduct { get; set; }
        public PaginatedList<BannerGroupsView> bannergroup { get; set;}
    }
}
