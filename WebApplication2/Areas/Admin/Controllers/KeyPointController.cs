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
using WebApplication2.Models.Products.KeyPoints;
using WebApplication2.Models.ViewModels.KeyPoints;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KeyPointController : BaseController
    {
               
        private IProducRepository productrepo;
        private IKeyPointRepository keypointrepo;
        private SignInManager<Operator> signin;

        public KeyPointController(UserManager<Operator> usermanager, SignInManager<Operator> signin, IProducRepository productrepo, IKeyPointRepository keypointrepo) : base(usermanager)
        {
            this.usermanager = usermanager;
            this.productrepo = productrepo;
            this.keypointrepo = keypointrepo;
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
        public async Task<IActionResult> List( int productid, KeyPointsTypes type)
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
                ViewBag.productid = productid;
                ViewBag.type = type;
                var keypointlist = await keypointrepo.SearchAsync(null, null, type, productid);
                var kpviewlist = new List<KeyPointView>();
                var persian = new PersianCalendar();
                if (keypointlist != null)
                {
                    foreach (var item in keypointlist)
                    {
                        kpviewlist.Add(new KeyPointView
                        {
                            id = item.id,
                            product = new Product
                            {
                                Id = item.product.Id,
                                PrimaryTitle = item.product.PrimaryTitle,

                            },
                            title = item.title,
                            type = item.types,
                            createDate = persian.persiandate(item.creatdatetime),
                            creator = item.creator.name + " " + item.creator.lastname,
                            lastModifyDate = item.lastmodifydate != null ? persian.persiandate((DateTime)item.lastmodifydate) : null,
                            lastModifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                        });
                    }
                }
                return View(kpviewlist);
            }
        }

        //   [Route("Admin/KeyPoint/Add",
        //Name = "pointadd")]
        [Authorize]
        public async Task<IActionResult> Add( int productid, KeyPointTypes type)
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
                ViewBag.type = type;
                ViewBag.productid = productid;
                return View();
            }
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id,  KeyPointTypes type)
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
                var keyp = await keypointrepo.FindAsync(id);
                ViewBag.productid = keyp.product.Id;
                ViewBag.type = keyp.types;
                return View("Add", keyp);
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete( int id)
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
                var keypoints = await keypointrepo.FindAsync(id);
                keypointrepo.DeleteAsync(keypoints);

                await keypointrepo.SaveAsync();
                return RedirectToAction("List", new { productid = keypoints.product.Id, type = keypoints.types });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save( int? id, int? productid, string Title, int Brand, States? State , KeyPointsTypes? type)
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
                    var product = await productrepo.FindAsync((int)productid);
                    var keypointz = new KeyPoint
                    {
                        product = new Product { Id = product.Id },
                        title = Title,
                        types = (KeyPointsTypes)type,
                        creator = new Operator
                        {
                            Id = this.Operator.Id
                        },
                        creatdatetime = DateTime.UtcNow

                    };
                    await keypointrepo.Addasync(keypointz);
                    await keypointrepo.SaveAsync();
                    return RedirectToAction("List", new { productid = keypointz.product.Id, type = keypointz.types });
                }
                //edit is okay
                else
                {
                    var user = await usermanager.FindByIdAsync(this.Operator.Id);
                    await keypointrepo.UpdateAsync(new KeyPoint
                    {
                        id = (int)id,
                        lastmodifier = user,
                        lastmodifydate = DateTime.UtcNow,
                        title = Title
                    });
                    await keypointrepo.SaveAsync();
                    var keypo = await keypointrepo.FindAsync((int)id);
                    return RedirectToAction("List", new { productid = keypo.product.Id, type = keypo.types });
                }
            }
        }           
    }
}
