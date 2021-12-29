using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Banners;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Groups;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Products.SpecialProduct;
using WebApplication2.Models.ViewModels.Groups;
using WebApplication2.Models.ViewModels.Home;
using WebApplication2.Models.ViewModels.Products;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private IBannerRepository bannerrepo;
        private ISpecialProductsRepository specialrepo;
        private IProductItemRepository productitemrepo;
        private IGroupRepository grouprepo;
        private IProducRepository productrepo;
        
       
        public HomeController(IBannerRepository bannerrepo, ISpecialProductsRepository specialrepo, IProductItemRepository productitemrepo, IGroupRepository grouprepo, IProducRepository productrepo)
        {
            this.bannerrepo = bannerrepo;
            this.specialrepo = specialrepo;
            this.productitemrepo = productitemrepo;
            this.grouprepo = grouprepo;
            this.productrepo = productrepo;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index( int? pageNumber)
        {
            var homeview = new HomeView();
            homeview.banners = new List<Banner>();
            var banner = await bannerrepo.Search(null);
            foreach (var item in banner)
            {
                homeview.banners.Add(new Banner { id = item.id, link = item.link, imgagesrc = $"{item.id}{item.ext}",ispecial=item.ispecial });
            }
            homeview.specialproduct = new List<SpecialView>();
            var specials = await specialrepo.Search(null);
            foreach (var item in specials)
            {
                var productitem = await productitemrepo.FindAsync(item.pnumb);
                var tags = productitem.itemtagvalue.Select(c => c.tagvalues).Select(x => x.tags).Take(5);
                homeview.specialproduct.Add(new SpecialView
                {
                    id = item.id,
                    brand = item.brand.title,
                    discount = Convert.ToString(item.discount),
                    date = Convert.ToString(item.leftedtime),
                    prnumb = item.pnumb,
                    price = Convert.ToString(item.price),
                    title = item.title,
                    imgurl = $"{productitem.product.Id}.jpg",
                    Tags=tags,
                    productnumb=productitem.product.Id,
                });
            }
            var bannergroups = await grouprepo.SearchBannerAsync(null, null);
            int i = 1;
            homeview.bannergroup = new List<BannerGroupsView>();
            foreach (var item in bannergroups)
            {
                homeview.bannergroup.Add(new BannerGroupsView
                {
                    id = item.id,
                    imgurl=item.groupnumber+item.ext,
                    groupnumber=item.groupnumber,
                    title=item.title
               });
                i++;
                if (i == 5) { break; }
            }
            var products = await productrepo.SearchAsync(null,null);
            var prlist = new PaginatedList<ProductView>();
            foreach (var item in products)
            {
                if (item.state == States.Enabled)
                {
                    prlist.Add(new ProductView
                    {
                        Id = item.Id,
                        PrimaryTitle = item.PrimaryTitle,
                    });
                }
            }
            int pageSize = 5;
            homeview.products = await PaginatedList<ProductView>.CreateAsync(prlist, pageNumber ?? 1, pageSize);
            return View(homeview);
        }
     
    }
}
