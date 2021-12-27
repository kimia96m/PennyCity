using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Comments;
using WebApplication2.Models.Products.Ratings;
using WebApplication2.Models.ViewModels.ProductLists;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    public class ProductController : Controller
    {
        private IProducRepository productrepos;
        public UserManager<Operator> usermg;
        private ICommentRepository commentrepo;
        public SignInManager<Operator> signin;
        private IRatingRepository ratingrepo;
        public ProductController(IProducRepository productrepo, UserManager<Operator> usermg, ICommentRepository commentrepo, SignInManager<Operator> signin,IRatingRepository ratingrepo)
        {
            productrepos = productrepo;
            this.usermg = usermg;
            this.commentrepo = commentrepo;
            this.signin = signin;
            this.ratingrepo = ratingrepo;
        }
        public async Task<IActionResult> Index(int id, int? pageNumber)
        {
            var product = await productrepos.DetailProduct(id);
            var rates = await ratingrepo.SearchProductItem(id);
            var comments = await commentrepo.Searchbypid(id);
            var list = new PaginatedList<Comment>();
            foreach (var item in comments)
            {
                list.Add(item);
            }
            int pageSize = 3;
            product.comments = await PaginatedList<Comment>.CreateAsync(list, pageNumber ?? 1, pageSize);
            if (rates.Count()!=0)
            {
                int x = 0;
                for (int i = 0; i < rates.Count(); i++)
                {
                    x += rates.ElementAt(i).Rate;
                }
                double y = x / rates.Count();
                ViewBag.rate = Math.Round(y);
            }
            else
            {
                ViewBag.rate = 0;
            }
            return View(product);
        }
        public async Task<ActionResult> List(string keyword,int? fromprice, int? toprice, int? brands,int[] specs, int? pageNumber)
        {
            var plist = await productrepos.SearchAsync(string.IsNullOrEmpty(keyword)?"":keyword, fromprice, toprice,  brands, specs);
            var productlist = new List<ProductListView>();
            foreach (var item in plist)
            {
                productlist.Add(new ProductListView
                {
                    Id = item.Id,
                    Primarytitle = item.PrimaryTitle,
                    Secoundarytitle = item.SecondaryTitle,
                    Imagename = $"{item.Id}.jpg",
                    Price = /*item.productitem.LastOrDefault().quantity == 0 ? "0" : item.productitem.LastOrDefault().price.ToString("N0"), درس 151 این خط بهتره ولی قلم کالا نباشه خطای نال میده درستش کن*/
                    item.productitem.Select(p => p.price).LastOrDefault().ToString("N0"),
                    brand=item.Brands,group=item.Groups
                });
            }
            int pageSize = 10;
            var list = await PaginatedList<ProductListView>.CreateAsync(productlist, pageNumber ?? 1, pageSize);
            return View(list);
        }
        public async Task<ActionResult> ProductListByGroup(int gid, int? pageNumber)
        {
            var plist = await productrepos.SearchAsync(gid);
            var productlist = new List<ProductListView>();
            foreach (var item in plist)
            {
                productlist.Add(new ProductListView
                {
                    Id = item.Id,
                    Primarytitle = item.PrimaryTitle,
                    Secoundarytitle = item.SecondaryTitle,
                    Imagename = $"{item.Id}.jpg",
                    Price = item.productitem.Select(p => p.price).LastOrDefault().ToString("N0"),
                    brand = item.Brands,
                    group = item.Groups
                });
            }
            ViewBag.gid = gid;
            int pageSize = 10;
            var list = await PaginatedList<ProductListView>.CreateAsync(productlist, pageNumber ?? 1, pageSize);
            return View("List", list);
        }
        [Authorize]
        public async Task<IActionResult> SendComment(string comment, int pid)
        {
            var user = await usermg.FindByNameAsync(User.Identity.Name);
            var claims = await usermg.GetClaimsAsync(user);
            if (claims.Any(c => c.Value != "customer"))
            {
                await signin.SignOutAsync();
                return View("SignIn", "Acount");
            }
            else
            {
                var comments = new Comment()
                {
                    productid = pid,
                    createdate = DateTime.UtcNow,
                    text = comment,
                    user = user
                };
                await commentrepo.Add(comments);
                await commentrepo.Save();
                return new RedirectResult("/Product/Index/" + pid);
            }
           
        }
        [Authorize]
        public async Task<IActionResult> SendRating(int rate, int pid)
        {
            var user = await usermg.FindByNameAsync(User.Identity.Name);
            var claims = await usermg.GetClaimsAsync(user);
            if (claims.Any(c => c.Value != "customer"))
            {
                await signin.SignOutAsync();
                return View("SignIn", "Acount");
            }
            else
            {
                var notfirstrate = await ratingrepo.FindProductItem(pid, user.name + " " + user.lastname);
                if (notfirstrate != null)
                {
                    var rating = new Rating
                    {
                        Id=notfirstrate.Id,
                        customer = user.name + " " + user.lastname,
                        ProductItemId = pid,
                        Rate = rate,
                    };
                    await ratingrepo.Update(rating);
                }
                else
                {
                    var rating = new Rating
                    {
                        customer = user.name + " " + user.lastname,
                        ProductItemId = pid,
                        Rate = rate,
                    };
                    await ratingrepo.AddAsync(rating);
                }
                await ratingrepo.Save();
                return new RedirectResult("/Product/Index/" + pid);
            }
           
        }

    }
}
