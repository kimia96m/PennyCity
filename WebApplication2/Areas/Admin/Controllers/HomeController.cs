using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Areas.Admin.Models.HomeView;
using WebApplication2.Models;
using WebApplication2.Models.Products;
using WebApplication2.Models.Products.Brands;
using WebApplication2.Models.Products.Groups;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Products.Specification;
using WebApplication2.Models.Products.Tags;
using WebApplication2.Models.Sellers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseController
    {
        private SignInManager<Operator> signin;
        private IBrandRepository brandrepo;
        private IGroupRepository grouprepo;
        private IProducRepository productrepo;
        private ISpecificationGroupsRepository specificationgroupsrepo;
        private ISpecificationRepository specificationrepo;
        private ISpecificationValuesRepository specificationvaluesrepo;
        private ITagRepository tagrepo;
        private ITagValuesRepository tagvaluerepo;
        private ISellerRepository sellerrepo;
        public HomeController(UserManager<Operator> usermanager, SignInManager<Operator> signin,   IBrandRepository brandrepo, IGroupRepository grouprepo, IProducRepository productrepo, ISpecificationGroupsRepository specificationgroupsrepo, ISpecificationRepository specificationrepo
        , ISpecificationValuesRepository specificationvaluesrepo
        , ITagRepository tagrepo, ITagValuesRepository tagvaluerepo, ISellerRepository sellerrepo) : base(usermanager)
        {
            this.signin = signin;
            this.usermanager = usermanager;
            this.brandrepo = brandrepo;
            this.grouprepo = grouprepo;
            this.productrepo = productrepo;
            this.specificationgroupsrepo = specificationgroupsrepo;
            this.specificationrepo = specificationrepo;
            this.specificationvaluesrepo = specificationvaluesrepo;
            this.tagrepo = tagrepo;
            this.tagvaluerepo = tagvaluerepo;
            this.sellerrepo=sellerrepo;
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
            return View();
        }
        [Authorize]
        public async Task<IActionResult> SearchedList(string searchedtext, int? pagenumber)
        {
            var user = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(user);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            var list = new List<HomeView>();
            var products = await productrepo.SearchAsync(searchedtext);
            var brandes = await brandrepo.SearchAsync(searchedtext, null, null);
            var groups = await grouprepo.SearchAsync(searchedtext);
            var sellers = await sellerrepo.Search(searchedtext);
            var specigroups = await specificationgroupsrepo.SearchAsync(searchedtext);
            var specis = await specificationrepo.SearchAsync(searchedtext);
            var specivalues = await specificationvaluesrepo.SearchAsync(searchedtext);
            var tags = await tagrepo.Search(searchedtext);
            var tagvalues = await tagvaluerepo.Search(searchedtext);
            foreach (var item in products)
            {
                list.Add(new HomeView
                {
                    id = item.Id,
                    name = item.PrimaryTitle,
                    uppercategoryname ="برند"+ " : " + item.Brands.title,
                    link = $"/admin/product/list/{item.Id}",
                    type ="کالا"
                });
            }
            foreach (var item in brandes)
            {
                list.Add(new HomeView
                {
                    id =(int) item.id,
                    name = item.title,
                    uppercategoryname = " ",
                    link = $"/admin/brand/list/{item.id}",
                    type="برند"
                });
            }
            foreach (var item in groups)
            {
                list.Add(new HomeView
                {
                    id = item.id,
                    name = item.title,
                    uppercategoryname = " ",
                    link = $"/admin/group/list/{item.id}",
                    type = "گروه"
                });
            }
            foreach (var item in sellers)
            {
                list.Add(new HomeView
                {
                    id = item.id,
                    name = item.title,
                    uppercategoryname = " ",
                    link = $"/admin/seller/list/{item.id}",
                    type = "فروشنده"
                });
            }
            foreach (var item in specigroups)
            {
                list.Add(new HomeView
                {
                    id = item.id,
                    name = item.title,
                    uppercategoryname ="گروه"+ " : " + item.group.title,
                    link = $"/admin/specification/groups/{item.id}",
                    type = "گروه مشخصات"
                });
            }
            foreach (var item in specis)
            {
                list.Add(new HomeView
                {
                    id = item.id,
                    name = item.title,
                    uppercategoryname ="گروه مشخصات فنی"+ " : " + item.specificationgroups.title,
                    link = $"/admin/specification/list/{item.id}",
                    type = "مشخصه فنی"
                });
            }
            foreach (var item in specivalues)
            {
                list.Add(new HomeView
                {
                    id = item.id,
                    name = item.valuetitle,
                    uppercategoryname ="مشخصه فنی"+ " : " + item.specification.title,
                    link = $"/admin/product/specificationlist/{item.id}",
                    type = "مقدار مشخصه فنی"
                });
            }
            foreach (var item in tags)
            {
                list.Add(new HomeView
                {
                    id = item.id,
                    name = item.title,
                    uppercategoryname = " ",
                    link = $"/admin/tag/list/{item.id}",
                    type = "برچسب"
                });
            }
            foreach (var item in tagvalues)
            {
                list.Add(new HomeView
                {
                    id = item.id,
                    name = item.title,
                    uppercategoryname =" برچسب"+" : "+ item.tags.title,
                    link = $"/admin/tag/value/{item.id}",
                    type = "مقدار برچسب"
                });
            }
            if (list.Count() == 0)
            {
                ViewBag.message = "جستجو بی نتیجه بود";
                
            }
                int pagesize = 2;
                var homelist = await PaginatedList<HomeView>.CreateAsync(list, pagenumber ?? 1, pagesize);
                return View(homelist);
            
        
        }
    }
}


