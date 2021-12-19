using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    public class AcountController : Controller
    {
        private UserManager<Operator> usermanager;
        private SignInManager<Operator> signinmanager;
        public AcountController(UserManager<Operator> usermanager, SignInManager<Operator> signinmanager)
        {
            this.signinmanager = signinmanager;
            this.usermanager = usermanager;

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(string email, string confirmpassword, string password, string firstname, string lastname)
        {
            //پسوردتو درست کن که به کاربر بگه نمیتونه عدد نباشه
            if(password.Equals(confirmpassword)&& !string.IsNullOrEmpty(email)&& !string.IsNullOrEmpty(firstname)&& !string.IsNullOrEmpty(lastname))
            {
                var customer = new Operator
                {
                    Email = email,
                    name = firstname,
                    lastname = lastname,
                    UserName = email
                };
                var signin = await usermanager.CreateAsync(customer, password);
                if (signin.Succeeded)
                {
                    var claims = await usermanager.AddClaimAsync(customer, new Claim( "UserType", "customer"));
                    if (claims.Succeeded)
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        ViewBag.error = "not find";
                        return View();
                    }
                   
                }
                else { ViewBag.error = "not find"; return View(); }
            }
            else { ViewBag.error = "not find"; return View(); }

        }

        public IActionResult SignIn() => View();


        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password, bool rememberme)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var user = await usermanager.FindByEmailAsync(email);
                if (user != null)
                {
                    var signin = await signinmanager.PasswordSignInAsync(user, password, rememberme, false);
                    if (signin.Succeeded)
                    {
                        var claimcheck = await usermanager.GetClaimsAsync(user);
                        if (claimcheck.Any(c => c.Value=="customer"))
                        {
                            return Redirect("~/");
                        }
                        
                    }
                }
            }
                return  View();
        }

 
             

        public async Task<IActionResult> SignOut()
        {
            await signinmanager.SignOutAsync();
            return Redirect("/");
        }

    }
}
