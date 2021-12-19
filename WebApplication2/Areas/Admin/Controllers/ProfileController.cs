using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Products.Groups;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileController : BaseController
    {
        private IHostingEnvironment eNv;
      
        public ProfileController(UserManager<Operator> userManager, /*IHostingEnvironment env,*/ IGroupRepository groupRepository) : base(userManager)
        {
            this.usermanager = userManager;
            
            //this.eNv = env;
        }
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult SignOut()
        {
            return View();
        }
        [Authorize]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult SignIn(string username, string password, bool rememberme)
        {
            return View();
        }
    }
}
