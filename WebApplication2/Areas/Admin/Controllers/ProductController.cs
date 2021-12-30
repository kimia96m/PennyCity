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
using WebApplication2.InfraStructure;
using WebApplication2.Models;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Brands;
using WebApplication2.Models.Products.Groups;
using WebApplication2.Models.Products.Specification;
using WebApplication2.Models.Products.Tags;
using WebApplication2.Models.ViewModels.Groups;
using WebApplication2.Models.ViewModels.ProductItems;
using WebApplication2.Models.ViewModels.Products;
using WebApplication2.Models.ViewModels.Specifications;
using WebApplication2.Models.ViewModels.Tags;
using Microsoft.AspNetCore.Authorization;
using WebApplication2.Models.ViewModels.Sellers;
using WebApplication2.Models.Sellers;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : BaseController
    {
        public IHostingEnvironment env;
        private UserManager<Operator> userManager;
        private IBrandRepository brandrepo;
        private IGroupRepository grouprepo;
        private IProducRepository productrepo;
        private ISpecificationGroupsRepository specificationgroupsrepo;
        private ISpecificationRepository specificationrepo;
        private ISpecificationValuesRepository specificationvaluesrepo;
        private ITagRepository tagrepo;
        private IProductItemRepository productitemrepo;
        private ITagValuesRepository tagvaluerepo;
        private SignInManager<Operator> signin;
        private ISellerRepository sellerrepo;

        public ProductController(UserManager<Operator> usermanager, ITagValuesRepository tagvaluerepo, IHostingEnvironment env, IProducRepository productrepo, IGroupRepository grouprepo, IBrandRepository brandrepo, ISpecificationValuesRepository specificationvaluesrepo, ISpecificationRepository specificationrepo, ISpecificationGroupsRepository specificationgroupsrepo, ITagRepository tagrepo, IProductItemRepository productitemrepo, SignInManager<Operator> signin, ISellerRepository sellerrepo) : base(usermanager)
        {
            this.tagvaluerepo = tagvaluerepo;
            this.productitemrepo = productitemrepo;
            this.tagrepo = tagrepo;
            this.env = env;
            this.specificationgroupsrepo = specificationgroupsrepo;
            this.specificationrepo = specificationrepo;
            this.specificationvaluesrepo = specificationvaluesrepo;
            this.brandrepo = brandrepo;
            this.productrepo = productrepo;
            this.grouprepo = grouprepo;
            userManager = usermanager;
            this.signin = signin;
            this.sellerrepo = sellerrepo;
        }
        // GET: /<controller>/
        [Authorize]
        public async Task<IActionResult> Index(int? id)
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
                var product = await productrepo.DetailProduct((int)id);

                return View(product);
            }
        }

        [Authorize]
        public async Task<IActionResult> List(int? id, string primaryTitle, int? pageNumber)
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
                breadcrumps = new List<Models.Shared.Breadcrump>(){
                new Models.Shared.Breadcrump { Title = "صفحه اصلی", Url = "/" },
                new Models.Shared.Breadcrump { Title = "لیست محصولات" }
                };
                var plist = await productrepo.SearchAsync(id, primaryTitle);
                var prlist = new List<ProductView>();
                var persian = new PersianCalendar();
                if (plist != null)
                {
                    foreach (var item in plist)
                    {
                        prlist.Add(new ProductView
                        {
                            Brands = new Brand { id = item.Brands.id, title = item.Brands.title },
                            Groups = new Group { id = item.Groups.id, title = item.Groups.title },
                            CreatDate = persian.persiandate(item.CreatDate),
                            Creator = item.Creator.name + " " + item.Creator.lastname,
                            LastModifyDate = item.LastModifyDate != null ? persian.persiandate((DateTime)item.LastModifyDate) : null,
                            LastModifier = item.LastModifier?.name + " " + item.LastModifier?.lastname,
                            Description = item.Description,
                            PrimaryTitle = item.PrimaryTitle,
                            SecondaryTitle = item.SecondaryTitle,
                            state = item.state,
                            Id = item.Id
                        });

                    }
                }
                int pageSize = 10;
                var list = await PaginatedList<ProductView>.CreateAsync(prlist, pageNumber ?? 1, pageSize);
                return View(list);
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
                var brandgroupmodel = new BrandGroupView();
                var brands = await brandrepo.SearchAsync(null, null, null);
                var selectbrands = brands.Select(b => new { b.title, b.id });
                brandgroupmodel.Brands = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectbrands, "id", "title");
                var groups = await grouprepo.SearchAsync(null, null, null);
                var selectgroups = groups.Select(g => new { g.title, g.id });
                brandgroupmodel.Groups = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectgroups, "id", "title");
                return View(brandgroupmodel);
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
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
                ViewBag.Id = id;
                var brandgroupmodel = new BrandGroupView();
                var brands = await brandrepo.SearchAsync(null, null, null);
                var groups = await grouprepo.SearchAsync(null, null, null);
                var product = await productrepo.FindAsync(id);
                brandgroupmodel.Products = new Product
                {
                    Brandid = product.Brandid,
                    Groupid = product.Groupid,
                    Description = product.Description,
                    state = product.state,
                    PrimaryTitle = product.PrimaryTitle,
                    SecondaryTitle = product.SecondaryTitle
                };
                var selectbrands = brands.Select(b => new { b.title, b.id });
                var selectgroups = groups.Select(g => new { g.title, g.id });
                var selectedbr = await brandrepo.FindAsync(brandgroupmodel.Products.Brandid);
                var selectedgr = await grouprepo.FindAsync(brandgroupmodel.Products.Groupid);
                brandgroupmodel.Brands = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectbrands, "id", "title", selectedbr.id);
                brandgroupmodel.Groups = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectgroups, "id", "title", selectedgr.id);
                return View("Add", brandgroupmodel);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save(int? id, string primaryTitle, string secondaryTitle, IFormFile photo, string description, int Brand, int Group, States? State)
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
                if (id == null)
                {

                    var product = new Product
                    {
                        Brandid = Brand,
                        Groupid = Group,
                        CreatDate = DateTime.UtcNow,
                        Creator = new Operator { Id = this.Operator.Id },
                        SecondaryTitle = secondaryTitle,
                        state = State,
                        PrimaryTitle = primaryTitle,
                        Description = description

                    };
                    await productrepo.AddAsync(product);
                    await productrepo.SaveAsync();
                    var productid = product.Id;
                    var ext = Path.GetExtension(photo.FileName);
                    var path = Path.Combine(env.WebRootPath + "\\Images\\Products", productid + ext);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await photo.CopyToAsync(filestream);

                        filestream.Close();
                    }

                    return RedirectToAction("List");
                }
                else
                {
                    var op = await usermanager.FindByIdAsync(this.Operator.Id);
                    var product = new Product
                    {
                        Brandid = Brand,
                        Groupid = Group,
                        LastModifyDate = DateTime.UtcNow,
                        LastModifier = op,
                        SecondaryTitle = secondaryTitle,
                        state = State,
                        PrimaryTitle = primaryTitle,
                        Description = description,
                        Id = (int)id

                    };
                    productrepo.UpdateAsync(product);
                    await productrepo.SaveAsync();
                    return RedirectToAction("List");
                }
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
                var product = await productrepo.FindAsync(id);
                if (product.productitem.Count != 0)
                {
                    TempData["message"] = "ابتدا آیتم های کالای مورد نظر را پاک کنید";

                }
                else if (product.specificationvalues.Count != 0)
                {
                    TempData["message"] = "ابتدا برچسب های کالای مورد نظر را پاک کنید";

                }
                else if (product.Keypoints.Count != 0)
                {
                    TempData["message"] = "ابتدا نقاط مثبت و منفی کالای مورد نظر را پاک کنید";

                }
                else
                {
                    await productrepo.DeleteAsync(product);
                    await productrepo.SaveAsync();
                    TempData["message"] = "عملیات با موفقیت انجام شد";
                }

                return RedirectToAction("List");
            }
        }
        [Authorize]
        public async Task<IActionResult> Specifications(int id, string title)
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

                ViewBag.id = id;

                var product = await productrepo.FindAsync(id);
                ViewBag.product = product;
                ViewBag.producttitlz = product.PrimaryTitle;
                PersianCalendar persian = new PersianCalendar();
                var specificationgroup = await specificationgroupsrepo.SearchAsync(product.Groups.id);
                var specificationgrouplist = new List<SpecificationGroupsView>();
                //var specificationg = await specificationgroupsrepo.SearchAsync(title, id);
                var counter = 0;
                foreach (var item in specificationgroup)
                {
                    specificationgrouplist.Add(new SpecificationGroupsView
                    {
                        id = item.id,
                        title = item.title,
                        createdate = persian.persiandate(item.createdate),
                        creator = item.creator.name + " " + item.creator.lastname,
                        state = item.state,
                        lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                        lastmodifydate = item.lastmodifydate != null ? persian.persiandate((DateTime)item.lastmodifydate) : null,
                        group = new GroupView
                        {
                            id = item.group.id,
                            title = item.group.title
                        },
                        specification = new List<SpecificationView>()

                    });
                    foreach (var x in item.specification)
                    {
                        specificationgrouplist[counter].specification.Add(new SpecificationView
                        {
                            id = x.id,
                            title = x.title
                        });

                    }

                    counter++;


                }
                return View(specificationgrouplist);
            }
        }
        [Authorize]
        public async Task<IActionResult> SpecificationList(int id, string sortOrder,string currentFilter,
           string searchString,int? pageNumber)
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
                ViewData["CurrentSort"] = sortOrder;
                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                var product = await productrepo.FindAsync(id);
                List<SpecificationValuesView> specivaluelist = new List<SpecificationValuesView>();
                foreach (var item in product.specificationvalues)
                {

                    specivaluelist.Add(new SpecificationValuesView
                    {
                        title = item.valuetitle,
                        id = item.id,
                        product = new ProductView
                        {
                            Id = id,
                            PrimaryTitle = item.product.PrimaryTitle
                        },
                        state = item.state,
                        specification = new SpecificationView
                        {
                            id = item.specification.id,
                            title = item.specification.title
                        },
                        lastmodifydate = Convert.ToString(item.lastmodifydate),
                        creator = item.creator.name + " " + item.creator.lastname,
                        createdate = Convert.ToString(item.createdate),
                        lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname

                    });
                }
                int pageSize = 10;
                var c = await PaginatedList<SpecificationValuesView>.CreateAsync(specivaluelist, pageNumber ?? 1, pageSize);
                return View(c);
             }
        }
        [Authorize]
        public async Task<IActionResult> DeleteSpecification(int id)
        {
            var user = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(user);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            else { 
                var specificationvalue = await specificationvaluesrepo.FindAsync(id);
            var product = await productrepo.FindAsync(specificationvalue.product.Id);
            await specificationvaluesrepo.DeleteAsync(id);
            await specificationvaluesrepo.SaveAsync();
            return RedirectToAction("SpecificationList", new { id = product.Id });
             }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveSpecifications(int pid, int[] ids, string[] value)
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
                var values = new List<SpecificationValues>();

                foreach (var id in ids)
                {
                    var user = await usermanager.FindByIdAsync(this.Operator.Id);
                    var param = Request.Form["value_" + id];
                    var specifis = await specificationrepo.FindAsync(id);
                    var product = await productrepo.FindAsync(pid);
                    values.Add(new SpecificationValues
                    {
                        valuetitle = param,
                        specification = specifis,
                        createdate = DateTime.UtcNow,
                        state = States.Enabled,
                        product = product,
                        creator=user

                    });
                    //values.Add(!string.IsNullOrEmpty(param)&&string.IsNullOrWhiteSpace(param)?param:null);
                }

                await specificationvaluesrepo.AddAsync(values);
                await specificationvaluesrepo.SaveAsync();
                return RedirectToAction("List");
            }
        }
        [Authorize]
        public async Task<IActionResult> Item(int id, States? state, int[] tagvalue,int? pagenumber)
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

                ViewBag.pid = id;
                var productitemlist = await productitemrepo.SearchAsync(id);
                var pitemtv = new List<ProductItemView>();
                var product = await productrepo.FindAsync(id);
                //ViewBag.Product = product;
                PersianCalendar persian = new PersianCalendar();


                var counter = 0;

                foreach (var item in productitemlist)
                {
                    var seller = await sellerrepo.Find(item.sellerid);
                    pitemtv.Add(new ProductItemView
                    {
                        createdate = persian.persiandate(item.createdate),
                        creator = item.creator.name + " " + item.creator.lastname,
                        discount = Convert.ToString(item.discount),
                        quantity = Convert.ToString(item.quantity),
                        lastmodifier = item.lastmodifier != null ? item.lastmodifier.name + " " + item.lastmodifier.lastname : "---",
                        lastmodifydate = item.lastmodifier != null ? persian.persiandate((DateTime)item.lastmodifydate) : "___",
                        product = product,
                        id = item.id,
                        price = Convert.ToString(item.price),
                        state = item.state,
                        itemtagvalue = new List<TagValueView>(),
                        seller= new SellerView() { id=item.sellerid,title=seller.title }
                    });
                    foreach (var x in item?.itemtagvalue)
                    {
                        pitemtv[counter].itemtagvalue.Add(new TagValueView
                        {
                            id = x.tagvalues.id,
                            title = x.tagvalues.title,
                            tagviews = new TagView { title = x.tagvalues.tags.title }

                        });
                    }
                    counter++;
                }
                int pagesize = 10;
                var list = await PaginatedList<ProductItemView>.CreateAsync(pitemtv, pagenumber ?? 1, pagesize);
                return View(list);
            }
        }
        [Authorize]
        public async Task<IActionResult> Additem(int pid)
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
                PersianCalendar persian = new PersianCalendar();
                ViewBag.pid = pid;
                var tags = await tagrepo.Search(null, null);
                var taglist = new List<TagView>();
                var counter = 0;
                foreach (var item in tags)
                {
                    taglist.Add(new TagView
                    {
                        id = item.id,
                        title = item.title,
                        state = item.state,
                        createdate = persian.persiandate(item.createdate),
                        creator = item.creator.name + " " + item.creator.lastname,
                        lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                        lastmodifydate = item.lastmodifydate != null ? persian.persiandate((DateTime)item.lastmodifydate) : null,
                        tagvalues = new List<TagValueView>()
                    });
                    foreach (var x in item.tagValues)
                    {
                        taglist[counter].tagvalues.Add(new TagValueView
                        {
                            id = x.id,
                            title = x.title,

                        });
                    }
                    counter++;
                }
                ViewBag.tagz = taglist;
                return View();
            }
        }
        [Authorize]
        public async Task<IActionResult> Edititem(int id)
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
                #region tagvalue
                PersianCalendar persian = new PersianCalendar();
                var tags = await tagrepo.Search(null, null);
                var taglist = new List<TagView>();
                var counter = 0;
                foreach (var item in tags)
                {
                    taglist.Add(new TagView
                    {
                        id = item.id,
                        title = item.title,
                        state = item.state,
                        createdate = persian.persiandate(item.createdate),
                        creator = item.creator.name + " " + item.creator.lastname,
                        lastmodifier = item.lastmodifier?.name + " " + item.lastmodifier?.lastname,
                        lastmodifydate = item.lastmodifydate != null ? persian.persiandate((DateTime)item.lastmodifydate) : null,
                        tagvalues = new List<TagValueView>()
                    });
                    foreach (var x in item.tagValues)
                    {
                        taglist[counter].tagvalues.Add(new TagValueView
                        {
                            id = x.id,
                            title = x.title
                        });
                    }
                    counter++;
                }
                ViewBag.tagz = taglist;
                #endregion
                var productitem = await productitemrepo.FindAsync(id);
                ViewBag.pid = productitem?.product?.Id;
                ViewBag.id = id;
                return View("Additem", productitem);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Saveitem(int? id, int? pid, double? price, float? discount, States? states, byte quantity, int[] tagvalues,int sellerid)
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
                    var productitemtagvalue = new List<ItemTagValue>();
                    var user = await usermanager.FindByIdAsync(this.Operator.Id);
                    var product = await productrepo.FindAsync((int)pid);
                    ViewBag.product = product.PrimaryTitle;
                    var productitem = new ProductItem
                    {
                        createdate = DateTime.UtcNow,
                        creator = user,
                        price = (double)price,
                        discount = (double)discount,
                        product = product,
                        quantity = quantity,
                        state = states,
                        sellerid=sellerid
                    };
                    await productitemrepo.AddAsync(productitem);
                    await productitemrepo.SaveAsync();
                    for (int i = 0; i < tagvalues.Length; i++)
                    {
                        productitemtagvalue.Add(new ItemTagValue
                        {
                            productitemid = productitem.id,
                            tagvalueid = tagvalues[i],
                        });
                    }

                    await productitemrepo.AddItemTagValuesAsync(productitemtagvalue);
                    await productitemrepo.SaveAsync();
                    return RedirectToAction("Item", new { id = pid });

                }
                else
                {
                    var user = await userManager.FindByIdAsync(this.Operator.Id);
                    var productitem = await productitemrepo.FindAsync((int)id);
                    var productitemtv = new List<ItemTagValue>();
                    for (int i = 0; i < tagvalues.Length; i++)
                    {
                        productitemtv.Add(new ItemTagValue
                        {
                            productitemid = (int)id,
                            tagvalueid = tagvalues[i]

                        });
                    }
                    await productitemrepo.itemtagvaluemerge(productitemtv);
                    await productitemrepo.SaveAsync();
                    await productitemrepo.UpdateAsync(new ProductItem
                    {
                        lastmodifydate = DateTime.UtcNow,
                        lastmodifier = user,
                        discount = discount,
                        price = (double)price,
                        quantity = quantity,
                        state = states,
                        id = (int)id,
                        sellerid=sellerid
                    });
                    await productitemrepo.SaveAsync();
                    ViewBag.id = pid;
                    return RedirectToAction("Item", new { id = pid });


                }
            }

        }


        [Authorize]
        public async Task<IActionResult> Deleteitem(int id)
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
                var pitem = await productitemrepo.FindAsync(id);
                //await productitemrepo.itemtagvaluemerge(pitem); اگه باید زیر مجموعه اش اول حذف بشه

                await productitemrepo.DeleteAsync(id);
                await productitemrepo.SaveAsync();
                return RedirectToAction("Item", new { id = pitem.product.Id });
            }
        }
    }

}
