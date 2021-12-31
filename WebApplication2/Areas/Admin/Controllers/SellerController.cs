using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.InfraStructure;
using WebApplication2.Models;
using WebApplication2.Models.Products.Ratings;
using WebApplication2.Models.Sellers;
using WebApplication2.Models.ViewModels.Sellers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SellerController : BaseController
    {
        private ISellerRepository sellerrepo;
        private SignInManager<Operator> signin;
        public IHostingEnvironment env;
        public SellerController(ISellerRepository sellerrepo, UserManager<Operator> userManager, SignInManager<Operator> signin,IHostingEnvironment env):base (userManager)
        {
            usermanager = userManager;
            this.sellerrepo = sellerrepo;
            this.signin = signin;
            this.env = env;
        }
        // GET: /<controller>/
        [Authorize]
        public async Task<IActionResult> List(int? id, string title, int? pageNumber, string producttitle)
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
                var p = new PersianCalendar();
                var sellers = await sellerrepo.Search(id, title, producttitle);
                var sellerslist = new List<SellerView>();
                if (sellers != null)
                {
                    foreach (var item in sellers)
                    {
                        sellerslist.Add(new SellerView
                        {
                            id = item.id,
                            title = item.title,
                            creator = item.creator.name + " " + item.creator.lastname,
                            createdate = p.persiandate(item.createdate),
                            paid = item.paid == null ? "0" : Convert.ToString(item.paid),
                            lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                            lastmodifydate= item.lastmodifydate!=null?p.persiandate((DateTime)item.lastmodifydate): null                        
                        });
                    }
                }
                int pagesize = 10;
                var list = await PaginatedList<SellerView>.CreateAsync(sellerslist, pageNumber ?? 1, pagesize);
                return View(list);
            }
            
        }
        [Authorize]
        public async Task<IActionResult> SearchedList(int? id, string title, int? pageNumber)
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
                var p = new PersianCalendar();
                var sellers = await sellerrepo.Search(id, title);
                var sellerslist = new List<SellerView>();
                if (sellers != null)
                {
                    foreach (var item in sellers)
                    {
                        sellerslist.Add(new SellerView
                        {
                            id = item.id,
                            title = item.title,
                            creator = item.creator.name + " " + item.creator.lastname,
                            createdate = p.persiandate(item.createdate),
                            paid = item.paid == null ? "0" : Convert.ToString(item.paid),
                            lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                            lastmodifydate = item.lastmodifydate != null ? p.persiandate((DateTime)item.lastmodifydate) : null
                        });
                    }
                }
                int pagesize = 10;
                var list = await PaginatedList<SellerView>.CreateAsync(sellerslist, pageNumber ?? 1, pagesize);
                return View("List",list);
            }

        }
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var iuser = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(iuser);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else { return View(); }
           
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id)
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
                ViewBag.Id = id;
                var p = new PersianCalendar();
               var seller= await sellerrepo.Find(id);
                var sellerfinal = new SellerView()
                {
                    id = seller.id,
                    title = seller.title,
                    paid = Convert.ToString(seller.paid),
                    creator = seller.creator.name + " " + seller.creator.lastname,
                    createdate = p.persiandate(seller.createdate),
                    lastmodifier = seller.lastmodifier?.name + " " + seller.lastmodifier?.lastname,
                    lastmodifydate = seller.lastmodifydate != null ? p.persiandate((DateTime)seller.lastmodifydate) : null,
                    description=seller.description
                    
                };
                return View("Add",sellerfinal);
            }
        }
        [Authorize]
        public async Task<IActionResult> Save(int? id,string title, int? todaypayment, string description,IFormFile photo)
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
                if (id == null)
                {
                   var seller= new Seller
                    {
                        title = title,
                        createdate = DateTime.UtcNow,
                        creator = this.Operator,
                        rating = new Rating { Rate = 0 },
                        description=description

                    };
                    await sellerrepo.Add(seller);
                    await sellerrepo.Save();
                    var sellerid = seller.id;
                    var ext = Path.GetExtension(photo.FileName);
                    var path = Path.Combine(env.WebRootPath + "\\Images\\Sellers", sellerid + ext);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await photo.CopyToAsync(filestream);

                        filestream.Close();
                    }
         
                    return View("Add");
                }
                else
                {
                    var op= await usermanager.FindByIdAsync(this.Operator.Id);
                    var seller= await sellerrepo.Find((int)id);
                    var sellers = new Seller {
                        id = (int)id,
                        title = title,
                        lastmodifier = op,
                        lastmodifydate = DateTime.UtcNow,
                        description = description,
                        paid = seller.paid != null ? seller.paid + todaypayment : todaypayment
                    };
                    sellerrepo.Update(sellers);
                   await sellerrepo.Save();
                    return RedirectToAction("List");
                }
                
            }
        }
        [Authorize]
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
            {// اگه حذفش کنی خطا نمیده؟؟

              
                var seller = await sellerrepo.Find(id);

                if (seller.rating != null)
                {
                    //TempData["massage"] = $" ابتدا لیست مشخصات فنی {y.title} را حذف کنید";
                }
                else
                {

                     sellerrepo.Delete(seller);
                    await sellerrepo.Save();
                    TempData["massage"] = $"حذف {seller.title} با موفقیت انجام شد";
                }

             
                return RedirectToAction("List");
            }
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var iuser = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(iuser);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else { return View(); }
                
        }

    }
}
