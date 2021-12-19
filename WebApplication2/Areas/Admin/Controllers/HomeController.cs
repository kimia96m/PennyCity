using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseController
    {
        private SignInManager<Operator> signin;

        public HomeController(UserManager<Operator> usermanager, SignInManager<Operator> signin) : base(usermanager)
        {
            this.signin = signin;
            this.usermanager = usermanager;
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
    }
}


