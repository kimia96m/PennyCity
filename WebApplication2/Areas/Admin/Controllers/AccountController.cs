using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers

{
    [Area("Admin")]
    public class AccountController : Controller
    {
        // GET: /<controller>/
        private UserManager<Operator> userManager;
        private SignInManager<Operator> signinmanager;
        public AccountController(UserManager<Operator> usermanager, SignInManager<Operator> signinmanager)
        {
            this.userManager = usermanager;
            this.signinmanager = signinmanager;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignIn(string ReturnUrl=null)
        {
            ViewBag.returnurl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password, bool rememberme, string ReturnUrl = null)
        {
            ViewBag.returnurl = ReturnUrl;
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                var result = await signinmanager.PasswordSignInAsync(user, password, rememberme, false);
                if (result.Succeeded)
                {

                    var claims = await userManager.GetClaimsAsync(user);
                    if (claims.Any(x => x.Value == "Operator"))
                    {

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        await signinmanager.SignOutAsync();
                        return RedirectToAction("SignIn", "Account");
                    }
               
                }
                else { ViewBag.error = "رمز عبور و نام کاربری غلط است"; }
            }
            ViewBag.error = "رمز عبور و نام کاربری غلط است";

            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await signinmanager.SignOutAsync();
            return Redirect("~/");
        }
    }
}
