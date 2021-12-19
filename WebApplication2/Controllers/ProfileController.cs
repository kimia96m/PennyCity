using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.InfraStructure;
using WebApplication2.Models;
using WebApplication2.Models.Orders;
using WebApplication2.Models.Products;
using WebApplication2.Models.Profiles;
using WebApplication2.Models.ViewModels.ProductItems;
using WebApplication2.Models.ViewModels.Products;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    public class ProfileController : Controller
    {
        private IAddressRepository addressrepo;
        private UserManager<Operator> usermanager;
        private IOrderRepository orderrepo;
        public ProfileController(IAddressRepository addressrepo, UserManager<Operator> usermanager, IOrderRepository orderrepo)
        {
            this.usermanager = usermanager;
            this.addressrepo = addressrepo;
            this.orderrepo = orderrepo;

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save( string ostaan, string city,string address,string tel)
        {
            var customer = await usermanager.FindByNameAsync(User.Identity.Name);
            await addressrepo.Add(new Address
            {
                city = city,
                customer = customer,
                location = address,
                province = ostaan,
                tel = tel
            });
            await addressrepo.Save();
            return RedirectToAction("Index", "Home");
            
        }
        public async Task<IActionResult> OrderHistory( int? pageNumber)
        {
            var customer = await usermanager.FindByNameAsync(User.Identity.Name);
            var orderlist = new List<OrderView>();
            var orders = await orderrepo.SearchbByCustomerId(customer.Id);
            var p = new PersianCalendar();
            var i = 0;
            foreach (var item in orders)
            {
                orderlist.Add(new OrderView
                {
                    id = item.id,
                    address = item.address,
                    fishnumber = item.fishnumber,
                    orderdate = Convert.ToString(p.persiandate(item.orderdate)),
                    orderitems = new List<OrderItemView>(),
                    //paymenttypes=item.paymenttypes,
                    registrationdate = Convert.ToString(item.registrationdate),
                    //state = EnumDescription.GetDescriptions(item.state.GetType()).ToString(),
                    totalprice = item.totalprice.ToString()

                });
                foreach (var x in item.orderitems)
                {
                    orderlist[i].orderitems.Add(new OrderItemView
                    {
                        id=x.id,
                        price=x.price.ToString(),
                        quantity=x.quantity,
                        productitem=new ProductItemView
                        {
                            id=x.productitem.id,
                            product=new Product
                            {
                                Id=x.productitem.product.Id,
                                PrimaryTitle=x.productitem.product.PrimaryTitle
                            }
                        }
                    });
                }
                i++;
            }
            int pageSize = 10;
            var list = await PaginatedList<OrderView>.CreateAsync(orderlist, pageNumber ?? 1, pageSize);
            return View(list);
        }
    }
}
