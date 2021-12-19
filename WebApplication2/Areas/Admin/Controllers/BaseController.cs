 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Areas.Admin.Models.Shared;
using WebApplication2.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: /<controller>/


        public List<Breadcrump> breadcrumps { get; set; }
        private Operator _operator;
        public UserManager<Operator> usermanager;
        //private SignInManager<Operator> signinmanager;
        public BaseController(UserManager<Operator> usermanager /*SignInManager<Operator> signinmanager*/)
        {
            this.usermanager = usermanager;
            //this.signinmanager = signinmanager;
        }


        private async Task<Operator> GetOperator()
        {
            _operator = await usermanager.FindByNameAsync("digiadmin");
            if (await usermanager.CheckPasswordAsync(_operator, "Kimi@123")) { return _operator; }
            else { return null; }
        }
        public Operator Operator
        {

            //get
            //{
            //    return GetOperator().GetAwaiter().GetResult();
            //}
            get
            {
                return GetOperator().GetAwaiter().GetResult();
            }
        }



    }

}
        
                 
            
            
    

