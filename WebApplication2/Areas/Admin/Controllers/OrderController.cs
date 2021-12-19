using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.InfraStructure;
using WebApplication2.Models;
using WebApplication2.Models.Orders;
using WebApplication2.Models.ViewModels.ProductItems;
using WebApplication2.Models.ViewModels.Sellers;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : BaseController
    {
        private IOrderRepository orderrepo;
        private SignInManager<Operator> signin;
        public OrderController(SignInManager<Operator> signin,UserManager<Operator> usermanager, IOrderRepository orderrepo):base(usermanager)
        {
            this.orderrepo = orderrepo;
            this.signin = signin;
            this.usermanager = usermanager;
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

                return View();
        }
        [Authorize]
        public async Task<IActionResult> ReviewList(int? pageNumber)
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
                var reviewingorderlist = await orderrepo.SearchbByIdAndState(OrderState.reviewing);
                var list = new List<OrderView>();
                var p = new PersianCalendar();
                var count = 0;
                foreach (var item in reviewingorderlist)
                {
                    list.Add(new OrderView
                    {
                        id = item.id,
                        address = item.address,
                        fishnumber = item.fishnumber,
                        orderdate = p.persiandate(item.orderdate),
                        customer = item.customer.name + " " + item.customer.lastname,
                        customerid = item.customer.Id,
                        orderitems = new List<OrderItemView>(),
                        totalprice=Convert.ToString(item.totalprice)
                        });
                    foreach (var y in item.orderitems)
                    {
                        list[count].orderitems.Add(new OrderItemView
                        {
                            id=y.id,
                            price= y.price.ToString("N0"),
                            productitem=new ProductItemView() {
                                id=y.productitem.id,
                                price= y.productitem.price.ToString("N0"),
                                product=y.productitem.product,
                                quantity=Convert.ToString(y.quantity),
                                //seller=new SellerView()
                                //{
                                //    id=y.productitem.seller!=null? y.productitem.seller.id:0,
                                //    title= y.productitem.seller != null ? y.productitem.seller.title : " "
                                //},
                                discount= Convert.ToString(y.productitem.discount)
                            },
                            quantity=y.quantity
                        });
                 
                    }
                    count++;
                }
                int pageSize = 5;
                var finallist = await PaginatedList<OrderView>.CreateAsync(list, pageNumber ?? 1, pageSize);
                return View(finallist);
            }
           
        }
        [Authorize]
        public async Task<IActionResult> ConfirmedList(int? pageNumber)
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
                var reviewingorderlist = await orderrepo.SearchbByIdAndState(OrderState.paid);
                var list = new List<OrderView>();
                var p = new PersianCalendar();
                var count = 0;
                foreach (var item in reviewingorderlist)
                {
                    
                        list.Add(new OrderView
                        {
                            id = item.id,
                            address = item.address,
                            fishnumber = item.fishnumber,
                            orderdate = p.persiandate(item.orderdate),
                            customer = item.customer.name + " " + item.customer.lastname,
                            customerid = item.customer.Id,
                            orderitems = new List<OrderItemView>(),
                            totalprice = Convert.ToString(item.totalprice)
                        });
                        foreach (var y in item.orderitems)
                        {
                            list[count].orderitems.Add(new OrderItemView
                            {
                                id = y.id,
                                price = y.price.ToString("N0"),
                                productitem = new ProductItemView()
                                {
                                    id = y.productitem.id,
                                    price = Convert.ToString(y.productitem.price),
                                    product = y.productitem.product,
                                    quantity =Convert.ToString(y.quantity),
                                    //seller = new SellerView()
                                    //{
                                    //    id = y.productitem.seller != null ? y.productitem.seller.id : 0,
                                    //    title = y.productitem.seller != null ? y.productitem.seller.title : " "
                                    //},
                                    discount = Convert.ToString(y.productitem.discount)
                                },
                                quantity = y.quantity
                            });
  
                        }
                        count++;
                    

                }
                int pageSize = 5;
                var finallist = await PaginatedList<OrderView>.CreateAsync(list, pageNumber ?? 1, pageSize);
                return View(finallist);
            }
           
        }
        [Authorize]
        public async Task<IActionResult> Save(int[] id)
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
                int i = 0;
                foreach (var item in id)
                {
                    var reviewed = await orderrepo.Find(id[i]);
                    var order = new Order() {
                        id=reviewed.id,
                        orderdate=reviewed.orderdate
                        ,address=reviewed.address
                        ,totalprice=reviewed.totalprice,customer=reviewed.customer,fishnumber=reviewed.fishnumber,orderitems=reviewed.orderitems,paymenttypes=reviewed.paymenttypes,registrationdate=reviewed.registrationdate,shippingtypes=reviewed.shippingtypes,
                        state=OrderState.paid
                    };
                    await orderrepo.Update(order);
                    await orderrepo.Save();
                    i++;
                }
                return RedirectToAction("ConfirmedList");
            }
          
        }
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            var iuser = await usermanager.FindByNameAsync(User.Identity.Name);
            var claims = await usermanager.GetClaimsAsync(iuser);
            if (claims.Any(c => c.Value != "Operator"))
            {
                await signin.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            return View();
        }

    }
}
