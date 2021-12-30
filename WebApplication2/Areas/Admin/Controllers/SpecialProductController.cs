using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Products.SpecialProduct;
using WebApplication2.Models.ViewModels.Products;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class SpecialProductController : Controller
    {
        private IProducRepository productrepo;
        private IProductItemRepository productitemrepo;
        private ISpecialProductsRepository specialrepo;
        private SignInManager<Operator> signin;
        private UserManager<Operator> usermanager;
        //private ibrandrepository 
        public SpecialProductController(IProducRepository productrepo, IProductItemRepository productitemrepo, ISpecialProductsRepository specialrepo, SignInManager<Operator> signin,UserManager<Operator> usermanager)
        {
            this.productrepo = productrepo;
            this.productitemrepo = productitemrepo;
            this.specialrepo = specialrepo;
            this.signin = signin;
            this.usermanager = usermanager;
        }
        // GET: /<controller>/
        [Authorize]
        public async Task<IActionResult> Index(int? pagenumber)
        {
            var iuser = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(iuser);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                var specials = await specialrepo.Search(null);
                var special = new List<SpecialView>();
                foreach (var item in specials)
                {
                    special.Add(new SpecialView
                    {
                        id = item.id,
                        title = item.title,
                        brand = item.brand.title,
                        price = Convert.ToString(item.price),
                        prnumb = item.pnumb,
                        discount = Convert.ToString(item.discount),
                        date = Convert.ToString(item.leftedtime),
                        daydate =Convert.ToString(item.lefteddays)
                        
                    });
                }
                int pagesize = 10;
                var list = await PaginatedList<SpecialView>.CreateAsync(special, pagenumber ?? 1, pagesize);
                return View(list);
            }
        }
        [Authorize]
        public async Task<IActionResult> Choose()
        {
            var iuser = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(iuser);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                var productitem = await productitemrepo.SearchAsync();
                var prlist = new List<SpecialView>();
                foreach (var item in productitem)
                {
                    prlist.Add(new SpecialView
                    {
                        title = item.product.PrimaryTitle,
                        state = item.state,
                        brand = item.product.Brands.title,
                        id = item.id,
                        price = Convert.ToString(item.price)
                    });
                }
                return View(prlist);
            }
        }
        [Authorize]
        public async Task<IActionResult> Aproove()
        {
            var iuser = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(iuser);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                var specialproduct = await specialrepo.Search(null);
                var splist = new List<SpecialView>();
                foreach (var item in specialproduct)
                {
                    splist.Add(new SpecialView
                    {
                        id = item.id,
                        prnumb = item.pnumb,
                        title = item.title,
                        price = Convert.ToString(item.price),
                        brand = item.brand.title,
                    });
                }
                return View(splist);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save(int[] id)
        {
            var iuser = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(iuser);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                int i = 0;

                foreach (var item in id)
                {
                    var sproduct = await productitemrepo.FindAsync(id[i]);
                    var m = await specialrepo.Find(id[i]);
                    //var t = await specialrepo.FindbyTitle(sproduct.product.PrimaryTitle);
                    if ((sproduct.state == States.Enabled) && (m == null))
                    {
                        var specialproduct = new SpecialProducts
                        {
                            pnumb = sproduct.id,
                            discount = (int)sproduct.discount,
                            brand = sproduct.product.Brands,
                            title = sproduct.product.PrimaryTitle,
                            state = sproduct.state,

                            price = sproduct.price
                        };
                        await specialrepo.Add(specialproduct);
                        await specialrepo.Save();

                    }

                    else
                    {
                        //پیام بده باید وضعیت کالا فعال باشه

                    }
                    i++;

                }
                return RedirectToAction("Aproove");
            }

        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> FinalSave(int[] id , int[] discount, DateTime[] starthour, DateTime[] endhour)
        {
            var iuser = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(iuser);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                int i = 0;

                foreach (var item in id)
                {
                    //var sproduct = await specialrepo.Find(id[i]);
                    //    var specialproduct = new SpecialProducts
                    //    {
                    //        discount = discount[i],                    
                    //        leftedtime = hours[i]
                    //    };
                    //    await specialrepo.Add(specialproduct);
                    //    await specialrepo.Save();



                    var specialproduct = new SpecialProducts
                    {
                        discount = discount[i],
                        lefteddays=Convert.ToString((endhour[i].Date - starthour[i].Date).TotalDays),
                        leftedtime = endhour[i].TimeOfDay - starthour[i].TimeOfDay,
                        id = id[i]
                    };
                    specialrepo.Update(specialproduct);
                    await specialrepo.Save();
                    i++;
                }
                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            var iuser = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(iuser);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                var q = await specialrepo.Find(id);
                await specialrepo.Delete(q);
                await specialrepo.Save();
                return RedirectToAction("Index");
            }
        }
    }
}
