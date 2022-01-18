using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Banners;
using WebApplication2.Models.ViewModels.Banners;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        // GET: /<controller>/
        private IBannerRepository bannerrepo;
        public IHostingEnvironment env;
        private SignInManager<Operator> signin;
        private UserManager<Operator> usermanager;
        public BannerController(IBannerRepository bannerrepo, IHostingEnvironment env, SignInManager<Operator> signin, UserManager<Operator> usermanager)
        {
            this.bannerrepo = bannerrepo;
            this.env = env;
            this.signin = signin;
            this.usermanager = usermanager;

        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(user);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        public async Task<IActionResult> List(int? id)
        {
            var user = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(user);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                var banner = await bannerrepo.Search(id);
                var bannerlist = new List<BannerView>();
                if (banner != null)
                {
                    foreach (var item in banner)
                    {
                        bannerlist.Add(new BannerView { id = item.id, Imagename = $"{item.id}{item.ext}",isspesial=item.ispecial.ToString() });
                    }
                }
                return View(bannerlist);
            }
        }
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var user = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(user);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(user);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                await bannerrepo.Delete(id);
                await bannerrepo.Save();
                return RedirectToAction("List");
            }
        }
        [Authorize]
        public async Task<IActionResult> Save(string link, IFormFile photo, string isspecial)
        {
            var user = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(user);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                var ispecial = await bannerrepo.SearchByIsSpecial(null);
                if (ispecial.Count()==0|| string.IsNullOrEmpty(isspecial))
                {
                    var banner = new Banner
                    {
                        link = link,
                        ext = Path.GetExtension(photo.FileName),
                        ispecial = isspecial == "on" ? true : false

                    };
                    await bannerrepo.Add(banner);
                    await bannerrepo.Save();

                    var bannerid = banner.id;
                    var ext = Path.GetExtension(photo.FileName);
                    if (banner.ispecial == true)
                    {
                        var path = Path.Combine(env.WebRootPath + "\\Images\\banners\\top-banner.png");
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await photo.CopyToAsync(filestream);

                            filestream.Close();
                        }
                    }
                    else
                    {
                        var path = Path.Combine(env.WebRootPath + "\\Images\\banners", bannerid + ext);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await photo.CopyToAsync(filestream);

                            filestream.Close();
                        }
                    }
                }
                else
                {
                    TempData["message"] = "بنر تاپ قبلی را حذف کنید";
                }
              
                return RedirectToAction("List");
            }
        }
    }
}
