using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Brands;
using WebApplication2.Repository.EF;
using WebApplication2.InfraStructure;
using WebApplication2.Models.ViewModels.Brands;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : BaseController
    {
        public IHostingEnvironment env;
        private IBrandRepository brandRepository;
        private IProducRepository productrepo;
        private SignInManager<Operator> signin;

        public BrandController(UserManager<Operator> usermanager, IHostingEnvironment env, IBrandRepository brandrepository, IProducRepository productrepo, SignInManager<Operator> signin) : base(usermanager)
        { 
            
            this.usermanager = usermanager;
            this.env = env;
            brandRepository = brandrepository;
            this.productrepo = productrepo;
            this.signin = signin;
            
        }
        // GET: /<controller>/
        [Authorize]
        public async Task<IActionResult> Index( int id)
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
                var user = this.Operator;
                return View();
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
            else
            {
                return View();
            }
        }
        [Authorize]
        public async Task<IActionResult> List(string title,int? id, States? state)
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
                //breadcrumps = new List<Models.Shared.Breadcrump>{ new Models.Shared.Breadcrump { Title="صفحه اصلی", Url="/"}, new Models.Shared.Breadcrump{ Title="لیست برندها" }};

                var brand = await brandRepository.SearchAsync(title, id, state);
                var brandlist = new List<BrandView>();
                PersianCalendar p = new PersianCalendar();

                if (brand != null)
                {
                    foreach (var item in brand)
                    {
                        brandlist.Add(new BrandView
                        {
                            id = item.id,
                            slug = item.slug,
                            title = item.title,
                            State = item.State,
                            createdatetime = p.persiandate(item.createdatetime),
                            creator = item.creator.name + " " + item.creator.lastname,
                            lastmodifydate = item.lastmodifydate != null ? p.persiandate((DateTime)item.lastmodifydate) : null,
                            lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                        });
                    }
                }

                return View(brandlist);
            }
        }       
        
        [HttpGet]
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
                PersianCalendar p = new PersianCalendar();
                var brands = await brandRepository.FindAsync(id);
                var brandFinal = new BrandView
                {
                    id = brands.id,
                    slug = brands.slug,
                    title = brands.title,
                    State = brands.State,
                    createdatetime = p.persiandate(brands.createdatetime),
                    creator = brands.creator.name + " " + brands.creator.lastname,
                    lastmodifydate = brands.lastmodifydate != null ? p.persiandate((DateTime)brands.lastmodifydate) : null,
                    lastmodifier = brands.lastmodifier?.name + " " + brands.lastmodifier?.lastname
                };
                return View("Add", brandFinal);
            }
        }
        [Authorize]
        public async Task< IActionResult> search(string primaryTitle, int? id, States? state)
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
                var brand = await brandRepository.SearchAsync(primaryTitle, id, state);
                return View("Brand");
            }
        }
        [HttpPost]
        [Authorize]
        public async Task <IActionResult> Save(int? id, string primaryTitle, string secondaryTitle, States? State ,IFormFile photo)
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

                    var brand = new Brand
                    {
                        createdatetime = DateTime.UtcNow,
                        creator = new Operator { Id = Operator.Id },
                        slug = secondaryTitle,
                        State = State,
                        title = primaryTitle
                    };


                    await brandRepository.AddAsync(brand);
                    await brandRepository.SaveAsync();
                    var brandid = brand.id;
                    var ext = Path.GetExtension(photo.FileName);
                    var path = Path.Combine(env.WebRootPath + "\\Images\\Brands", brandid + ext);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await photo.CopyToAsync(filestream);

                        filestream.Close();
                    }

                    return View("Index");
                }
                else
                {
                    await brandRepository.Update(new Brand
                    {
                        id = (int)id,
                        lastmodifier = this.Operator,
                        slug = secondaryTitle,
                        State = State,
                        title = primaryTitle


                    });
                    await brandRepository.SaveAsync();
                    return RedirectToAction("List");
                }
            }
         
        }
        //public async Task<IActionResult> Update(Brand brand)
        //{
        //    var br = await  Content.brand.findasync(brand);
        //    br.id = brand.id;
        //    br.lastmodifier = brand.lastmodifier;
        //    br.lastmodifydate = brand.lastmodifydate;
        //    br.slug = brand.slug;
        //    br.state = brand.State;
        //    br.title = brand.title;
        //}
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
            {
                var brand = new Brand
                {
                    id = id
                };
                var product = await productrepo.FindbrAsync(id);
                string producttitles = "";
                foreach (var item in product)
                {
                    producttitles = producttitles + item.PrimaryTitle + " و ";
                }
                if (product != null)
                {
                    TempData["message"] = $" ابتدا برند {producttitles} را تغییر دهید";
                }
                else
                {
                    TempData["message"] = "تغییرات با موفقیت انجام شد";
                    await brandRepository.DeleteAsync(brand);
                    await brandRepository.SaveAsync();
                }

                return RedirectToAction("List");
            }
        }
    }
}
