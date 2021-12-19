using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Areas.Admin.Models.KeyPoints;
using WebApplication2.InfraStructure;
using WebApplication2.Models;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Groups;
using WebApplication2.Models.Products.Specification;
using WebApplication2.Models.ViewModels.Groups;
using WebApplication2.Models.ViewModels.Specifications;
using WebApplication2.Repository.EF;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecificationController : BaseController
    {
        // GET: /<controller>/
        private ISpecificationGroupsRepository specifigrouprepo;
        private ISpecificationRepository specifirepo;
        private ISpecificationValuesRepository specifivaluerepo;
        private IGroupRepository grouprepo;
        private SignInManager<Operator> signin;
        public SpecificationController(UserManager<Operator> usermanager, IGroupRepository grouprepo, ISpecificationGroupsRepository specifigrouprepo, ISpecificationValuesRepository specifivaluerepo, ISpecificationRepository specifirepo, SignInManager<Operator> signin) : base(usermanager)
        {
            this.grouprepo = grouprepo;
            this.specifirepo = specifirepo;
            this.specifivaluerepo = specifivaluerepo;
            this.specifigrouprepo = specifigrouprepo;
            this.usermanager = usermanager;
            this.signin = signin;
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
            else
            {
                return View();
            }
        }
        [Authorize]
        public async Task<IActionResult> List(int? id, string title)
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
                ViewBag.specigid = id;
                ViewBag.title = title;
                PersianCalendar persian = new PersianCalendar();
                var specificationlist = new List<SpecificationView>();
                var specification = await specifirepo.SearchAsync(title, (int)id);
                foreach (var item in specification)
                {
                    specificationlist.Add(new SpecificationView
                    {
                        title = item.title,
                        state = item.state,
                        id = item.id,
                        creator = item.creator.name + " " + item.creator.lastname,
                        createdate = persian.persiandate(item.createdate),
                        lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                        lastmodifydate = item.lastmodifydate != null ? persian.persiandate((DateTime)item.lastmodifydate) : null

                    });


                }

                return View(specificationlist);
            }
        }
        [Authorize]
        public async Task<IActionResult> Groups(int id, string title, States state)
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
                ViewBag.gid = id;
                ViewBag.title = title;
                var persian = new PersianCalendar();
                var specificationgrouplist = new List<SpecificationGroupsView>();
                var speicis = await specifigrouprepo.FindAsync((int)id);
                //await speicis.group.id;
                var specigroup = await specifigrouprepo.SearchAsync(title, id);
                foreach (var item in specigroup)
                {
                    specificationgrouplist.Add(new SpecificationGroupsView
                    {
                        title = item.title,
                        createdate = persian.persiandate(item.createdate),
                        creator = item.creator?.name + " " + item.creator?.lastname,
                        lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                        lastmodifydate = item.lastmodifydate != null ? persian.persiandate((DateTime)item.lastmodifydate) : null,
                        id = item.id,
                        state = item.state,
                        group = new GroupView
                        {
                            id = item.group.id,
                            title = item.group.title
                        }
                    });
                }
                return View(specificationgrouplist);

            }
         
        }
        [Authorize]
        public async Task<IActionResult> Addgroup(int? gid)
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
                ViewBag.gid = gid;
                return View();
            }
        }
        [Authorize]
        public async Task<IActionResult> Editgroup(int id)
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
                ViewBag.id = id;
                var specifigroup = await specifigrouprepo.FindAsync(id);
                ViewBag.gid = specifigroup.group.id;
                return View("Addgroup", specifigroup);
            }
        }
        [Authorize]
        public async Task<IActionResult> Deletegroup(int id)
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
                var specigroup = await specifigrouprepo.FindAsync(id);
                if (/*specigroup.specification != null||*/ specigroup.specification.Count != 0)
                {
                    TempData["massage"] = "ابتدا زیر شاخه هایش زا پاک کنید";
                }
                else
                {
                    await specifigrouprepo.Delete(id);
                    await specifigrouprepo.SaveAsync();
                    TempData["massage"] = "عملیات با موفقیت انجام شد";

                }


                return RedirectToAction("Groups", new { id = specigroup.group.id });
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Savegroup(int? gid, string Title, int? id, States? state)
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
                    var user = await usermanager.FindByIdAsync(this.Operator.Id);
                    var group = await grouprepo.FindAsync((int)gid);
                    await specifigrouprepo.AddAsync(new SpecificationGroups
                    {
                        createdate = DateTime.UtcNow,
                        creator = user,
                        group = group,
                        state = (States)state,
                        title = Title,
                    });
                    await specifigrouprepo.SaveAsync();
                    return RedirectToAction("Groups", new { id = gid });
                }
                else
                {
                    var user = await usermanager.FindByIdAsync(this.Operator.Id);
                    await specifigrouprepo.UpdateAsync(new SpecificationGroups
                    {
                        id = (int)id,
                        title = Title,
                        lastmodifier = user,
                        state = (States)state,
                        lastmodifydate = DateTime.UtcNow

                    });
                    await specifigrouprepo.SaveAsync();
                    return RedirectToAction("Groups", new { id = gid });
                }
            }
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
                ViewBag.id = id;
                var specifi = await specifirepo.FindAsync(id);
                var specificationgroup = new SpecificationView
                {
                    state = specifi.state,
                    title = specifi.title
                };
                ViewBag.specigid = specifi.specificationgroups.id;

                return View("Add", specificationgroup);
            }
        }
        [Authorize]
        public async Task<IActionResult> Add(int? specigid)
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
                ViewBag.specigid = specigid;
                return View();
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
                var specification = await specifirepo.FindAsync(id);
                if (specification.specificationvalues.Count != 0)
                {
                    TempData["massage"] = "محصولات را پاک کن";

                }
                else
                {
                    await specifirepo.DeleteAsync(id);
                    await specifirepo.SaveAsync();
                    TempData["massage"] = "عملیات با موفقیت انجام شد";
                }

                return RedirectToAction("List", new { id = specification.specificationgroups.id });
            }
        }
        [Authorize]
        public async Task<IActionResult> Save(int? id, int? specigid, string Title, States? State)
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
                    var user = await usermanager.FindByIdAsync(this.Operator.Id);
                    var groupspeci = await specifigrouprepo.FindAsync((int)specigid);
                    await specifirepo.AddAsync(new Specification
                    {
                        title = Title,
                        state = (States)State,
                        creator = user,
                        createdate = DateTime.UtcNow,
                        specificationgroups = groupspeci,



                    }
                        );
                    await specifirepo.SaveAsync();
                    return RedirectToAction("List", new { id = specigid });
                }
                else
                {
                    var user = await usermanager.FindByIdAsync(this.Operator.Id);

                    await specifirepo.UpdateAsync(new Specification
                    {
                        id = (int)id,
                        title = Title,
                        state = (States)State,
                        lastmodifier = user,
                        lastmodifydate = DateTime.UtcNow
                    }
                        );
                    await specifirepo.SaveAsync();
                    return RedirectToAction("List", new { id = specigid });
                }

            }
        }
    }
}
