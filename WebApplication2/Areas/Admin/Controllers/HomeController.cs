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
        // GET: /<controller>/
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
        public async Task<IActionResult> SearchedList(string searchedtext)
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
            return View();
        }
    }
}


