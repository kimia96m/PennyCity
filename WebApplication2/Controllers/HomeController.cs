﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Banners;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Products.SpecialProduct;
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
       
        public HomeController(IBannerRepository bannerrepo, ISpecialProductsRepository specialrepo, IProductItemRepository productitemrepo)
        {
            this.bannerrepo = bannerrepo;
            this.specialrepo = specialrepo;
            this.productitemrepo = productitemrepo;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
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
            return View(homeview);
        }
     
    }
}
