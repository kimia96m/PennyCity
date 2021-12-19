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
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Groups;
using WebApplication2.Models.Products.Specification;
using WebApplication2.Models.ViewModels.Groups;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupController : BaseController
    {
        private IHostingEnvironment eNv;
        private IGroupRepository grouprepos;
        private ISpecificationRepository specirepo;
        private SignInManager<Operator> signin;
        public GroupController(UserManager<Operator> userManager, ISpecificationRepository specirepo, IHostingEnvironment env, IGroupRepository groupRepository, SignInManager<Operator> signin) : base(userManager)
        {
            usermanager = userManager;
            grouprepos = groupRepository;
            this.specirepo = specirepo;
            this.signin = signin;
            eNv = env;
        }
        // GET: /<controller>/
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
            else
            {
                return View();
            }
        }
        [Authorize]
        public async Task<IActionResult>  Add()
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
        public async Task<IActionResult> List(string title, int? id, States? state)
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

                var group = await grouprepos.SearchAsync(title, id, state);
                var grouplist = new List<GroupView>();
                PersianCalendar p = new PersianCalendar();

                if (group != null)
                {
                    foreach (var item in group)
                    {
                        grouplist.Add(new GroupView
                        {
                            id = item.id,
                            slug = item.slug,
                            title = item.title,
                            State = item.State,
                            createdatetime = p.persiandate(item.createdatetime),
                            creator = item.creator?.name + " " + item.creator?.lastname,
                            lastmodifydate = item.lastmodifydate != null ? p.persiandate((DateTime)item.lastmodifydate) : null,
                            lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                        });
                    }
                }

                return View(grouplist);
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
                var groups = await grouprepos.FindAsync(id);
                var brandFinal = new GroupView
                {
                    id = groups.id,
                    slug = groups.slug,
                    title = groups.title,
                    State = groups.State,
                    createdatetime = p.persiandate(groups.createdatetime),
                    creator = groups.creator.name + " " + groups.creator.lastname,
                    lastmodifydate = groups.lastmodifydate != null ? p.persiandate((DateTime)groups.lastmodifydate) : null,
                    lastmodifier = groups.lastmodifier?.name + " " + groups.lastmodifier?.lastname
                };
                return View("Add", brandFinal);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult>  Save(int? id, string primaryTitle, string secondaryTitle, States? State)
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
                {//add
                    await grouprepos.AddAsync(new Group
                    {
                        createdatetime = DateTime.UtcNow,
                        creator = this.Operator,
                        title = primaryTitle.stringisnull() ? null : primaryTitle,
                        slug = secondaryTitle.stringisnull() ? null : secondaryTitle,
                        State = State
                    });
                    await grouprepos.SaveAsync();
                    return View("Add");
                }
                else
                {

                    await grouprepos.Update(new Group
                    {
                        id = (int)id,
                        lastmodifier = this.Operator,
                        slug = secondaryTitle,
                        State = State,
                        title = primaryTitle


                    });
                    await grouprepos.SaveAsync();
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
            {
                var brand = new Group
                {
                    id = id
                };
                var y = await grouprepos.FindAsync(brand.id);

                if (y.specificationgroups != null)
                {
                    TempData["massage"] = $" ابتدا لیست مشخصات فنی {y.title} را حذف کنید";
                }
                else
                {
                    await grouprepos.DeleteAsync(brand);
                    await grouprepos.SaveAsync();
                    TempData["massage"] = $"حذف {y.title} با موفقیت انجام شد";
                }

                return RedirectToAction("List");
            }
        }
        [Authorize]
        public async Task<IActionResult> AddBanner()
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
        public async Task<IActionResult> SaveBanner(int? id, IFormFile photo,int groupnumber)
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
                    var group = await grouprepos.FindAsync(groupnumber);
                    if (group != null)
                    {
                        var banner = await grouprepos.FindBannernumberAsync(groupnumber);
                        if (group.State == States.Enabled && banner == null)
                        {
                            var bannergroups = new BannerGroups
                            {
                                createdatetime = DateTime.UtcNow,
                                creator = Operator,
                                title = group.title,
                                ext = Path.GetExtension(photo.FileName),
                                groupnumber = groupnumber
                            };
                            await grouprepos.AddbannerAsync(bannergroups);
                            await grouprepos.SaveAsync();
                            var productid = bannergroups.groupnumber;
                            var ext = Path.GetExtension(photo.FileName);
                            var path = Path.Combine(eNv.WebRootPath + "\\Images\\Groups", productid + ext);
                            using (var filestream = new FileStream(path, FileMode.Create))
                            {
                                await photo.CopyToAsync(filestream);

                                filestream.Close();
                            }
                            return View("AddBanner");
                        }
                        else
                        {
                            ViewBag.error = "گروه قبلا افزوده شده و یا وضعیت گروه فعال نیست";
                            return View("AddBanner");
                        }
               
                    }
                    else
                    {
                        ViewBag.error = "گروهی با این شناسه وجود ندارد";
                        return View("AddBanner");
                    }
                }
                else
                {
                    var group = await grouprepos.FindAsync(groupnumber);
                    if (group != null)
                    {
                        var banner = await grouprepos.FindBannernumberAsync(groupnumber);
                    if (group.State == States.Enabled && banner==null)
                    {
                        var bannergroup = new BannerGroups
                        {
                            id = (int)id,
                            lastmodifier = Operator,
                            groupnumber = groupnumber,
                            ext = Path.GetExtension(photo.FileName),
                            title = group.title,
                            lastmodifydate = DateTime.UtcNow
                        };
                        await grouprepos.UpdateBanner(bannergroup);
                        await grouprepos.SaveAsync();
                        var productid = bannergroup.groupnumber;
                        var ext = Path.GetExtension(photo.FileName);
                        var path = Path.Combine(eNv.WebRootPath + "\\Images\\Groups", productid + ext);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await photo.CopyToAsync(filestream);

                            filestream.Close();
                        }
                        return RedirectToAction("ListBanner");
                    }
                    else
                    {
                        ViewBag.error = "گروه قبلا افزوده شده و یا وضعیت گروه فعال نیست";
                            return View("AddBanner");
                        }
                    }
                    else
                    {
                        ViewBag.error = "گروهی با این شناسه وجود ندارد";
                        return View("AddBanner");
                    }
                }
        }

        }
        [Authorize]
        public async Task<IActionResult> ListBanner(string title, int? id, int? pageNumber)
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

                var group = await grouprepos.SearchBannerAsync(title, id);
                var grouplist = new List<BannerGroupsView>();
                PersianCalendar p = new PersianCalendar();

                if (group != null)
                {
                    foreach (var item in group)
                    {
                        grouplist.Add(new BannerGroupsView
                        {
                            id = item.id,
                            groupnumber=item.groupnumber,
                            imgurl = $"{item.groupnumber}{item.ext}",
                            title = item.title,
                            createdatetime = p.persiandate(item.createdatetime),
                            creator = item.creator?.name + " " + item.creator?.lastname,
                            lastmodifydate = item.lastmodifydate != null ? p.persiandate((DateTime)item.lastmodifydate) : null,
                            lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                        });
                    }
                }
                int pageSize = 10;
                var list = await PaginatedList<BannerGroupsView>.CreateAsync(grouplist, pageNumber ?? 1, pageSize);
                return View(list);
            }
        }
        [Authorize]
        public async Task<IActionResult> DeleteBanner(int id)
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
                var y = await grouprepos.FindBannerAsync(id);
                    await grouprepos.DeleteBannerAsync(y);
                    await grouprepos.SaveAsync();
                    TempData["massage"] = $"حذف {y.title} با موفقیت انجام شد";              
                return RedirectToAction("ListBanner");
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditBanner(int id)
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
                var groups = await grouprepos.FindBannerAsync(id);
                var brandFinal = new BannerGroupsView
                {
                    id = groups.id,
                    title = groups.title,
                    groupnumber=groups.groupnumber,
                    createdatetime = p.persiandate(groups.createdatetime),
                    creator = groups.creator.name + " " + groups.creator.lastname,
                    lastmodifydate = groups.lastmodifydate != null ? p.persiandate((DateTime)groups.lastmodifydate) : null,
                    lastmodifier = groups.lastmodifier?.name + " " + groups.lastmodifier?.lastname
                };
                return View("AddBanner", brandFinal);
            }
        }
    }
}
