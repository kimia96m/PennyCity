using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.InfraStructure;
using WebApplication2.Models;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Tags;
using WebApplication2.Models.ViewModels.Tags;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : BaseController
    {
        private ITagRepository tagrepo;
        private ITagValuesRepository tagvaluerepo;
        private SignInManager<Operator> signin;
        
        public TagController(UserManager<Operator> usermanager, SignInManager<Operator> signin, ITagRepository tagrepo, ITagValuesRepository tagvaluerepo) :base(usermanager)
        {
            this.tagvaluerepo = tagvaluerepo;
            this.usermanager = usermanager;
            this.tagrepo = tagrepo;
            this.signin = signin;
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
        public async Task<IActionResult> List( string title, States state, int? pagenumber)
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
                // اول لیستی از تگ ها رو سرچ میکنیم. بعد یه نمونه لیستی از تگ ویو میسازیم که قراره بره سمت کلاینت. بعد اگر لیست اولمون خالی نبود به ازای هر فیلدی
                // در لیست دوم با یه فور ایچ از تو لیست اول پرش میکنیم. در نهایت لیست پر شده که قراره بره سمت ویو رو ریترن کردیم
                var taglist = await tagrepo.Search(null, null);
                var tagviewlist = new List<TagView>();
                var persian = new PersianCalendar();
                if (taglist != null)
                {
                    foreach (var item in taglist)
                    {
                        tagviewlist.Add(new TagView
                        {
                            id = item.id,
                            title = item.title,
                            createdate = persian.persiandate(item.createdate),
                            creator = item.creator.name + " " + item.creator.lastname,
                            lastmodifydate = item.lastmodifydate != null ? persian.persiandate((DateTime)item.lastmodifydate) : null,
                            lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                            state = item.state
                        });
                    }
                }
                int pagesize = 10;
                var list = await PaginatedList<TagView>.CreateAsync(tagviewlist, pagenumber ?? 1, pagesize);
                return View(list);
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
                var tag = await tagrepo.Find(id);
                return View("Add", tag);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save(int? id, string title, States? state)
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
                    await tagrepo.Add(new Tag
                    {
                        creator = new Operator { Id = this.Operator.Id },
                        createdate = DateTime.UtcNow,
                        state = state,
                        title = title

                    });
                    await tagrepo.Save();
                    return RedirectToAction("List");
                }
                else
                {
                    var user = await usermanager.FindByIdAsync(this.Operator.Id);
                    await tagrepo.Update(new Tag { id = (int)id, state = state, title = title, lastmodifydate = DateTime.UtcNow, lastmodifier = user });
                    await tagrepo.Save();
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
                var tag = await tagrepo.Find(id);
                if (tag.tagValues.Count != 0)
                {
                    TempData["message"] = $"ابتدا مقادیر برچسب {tag.title} را پاک کنید";
                }
                else
                {
                    await tagrepo.Delete(id);
                    await tagrepo.Save();
                    TempData["message"] = "تغییرات با موفقیت انجام شد";

                }

                return RedirectToAction("List");
            }
        }
        [Authorize]
        public async Task<IActionResult> Value(int id,string title, States? state)
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
                PersianCalendar persian = new PersianCalendar();
                var tagvaluelist = new List<TagValueView>();
                var tagvalue = await tagvaluerepo.Search(null, null, id);
                if (tagvalue != null)
                {
                    foreach (var item in tagvalue)
                    {
                        tagvaluelist.Add(new TagValueView
                        {
                            id = item.id,
                            title = item.title,
                            createdate = persian.persiandate(item.createdate),
                            creator = item.creator.name + " " + item.creator.lastname,
                            lastmodifydate = item.lastmodifydate != null ? persian.persiandate((DateTime)item.lastmodifydate) : null,
                            lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                            state = item.state,
                            tagviews = new TagView
                            {
                                id = item.tags.id,
                                title = item.tags.title

                            }
                        });

                    }

                }
                return View(tagvaluelist);
            }
        }
        [Authorize]
        public async Task<IActionResult> Addvalue(int tagid)
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
                ViewBag.tagid = tagid;
                ViewBag.tag = await tagrepo.Find(tagid);
                return View();
            }
        }
        [Authorize]
        public async Task<IActionResult> Editvalue(int id)
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

                var tagvalue = await tagvaluerepo.Find(id);
                ViewBag.tagid = tagvalue?.tags?.id;

                return View("Addvalue", tagvalue);
            }
        }
        [Authorize]
        public async Task<IActionResult> Deletevalue(int id)
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
                var tagvalue = await tagvaluerepo.Find(id);
                await tagvaluerepo.Delete(id);
                await tagvaluerepo.Save();
                return RedirectToAction("Value", new { id = tagvalue.tags.id });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Savevalue(int? id,int tagid, States? state, string title)
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
                    var tag = await tagrepo.Find((int)tagid);
                    await tagvaluerepo.Add(new TagValeus
                    {
                        createdate = DateTime.UtcNow,
                        creator = user,
                        state = state,
                        tags = tag,
                        title = title

                    });
                    await tagvaluerepo.Save();
                    return RedirectToAction("Value", new { id = tag.id });
                }
                else
                {
                    var user = await usermanager.FindByIdAsync(this.Operator.Id);
                    await tagvaluerepo.Update(new TagValeus { id = (int)id, state = state, title = title, lastmodifydate = DateTime.UtcNow, lastmodifier = user });
                    await tagvaluerepo.Save();
                    return RedirectToAction("Value", new { id = tagid });
                }
            }
        }

    }
}
