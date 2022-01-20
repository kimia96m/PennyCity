using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Orders;
using WebApplication2.Models.Products.Carts;
using WebApplication2.Models.Products.ProductItems;
using WebApplication2.Models.Profiles;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    public class OrderController : Controller
    {
        private ICartRepository cartrepo;
        private IAddressRepository addressrepo;
        private UserManager<Operator> usermanager;
        private IOrderRepository orderrepo;
        private IProductItemRepository pitemrepo;
        public OrderController(ICartRepository cartrepo, UserManager<Operator> usermanager,IAddressRepository addressrepo, IOrderRepository orderrepo, IProductItemRepository pitemrepo)
        {
            this.addressrepo = addressrepo;
            this.usermanager = usermanager;
            this.cartrepo = cartrepo;
            this.orderrepo = orderrepo;
            this.pitemrepo = pitemrepo;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var customer = await usermanager.FindByNameAsync(User.Identity.Name);
            var cart = await cartrepo.Find(customer.Id);
            if (cart != null)
            {
                var totalpricecart = cart.cartitem.Sum(c => c.productitem.price * c.quantity).ToString("N0");
                ViewBag.Totalpricecart = totalpricecart;
                var addres = await addressrepo.Find(customer.Id);


                return View(addres);
            }
            else
            {
                var address = new List<Address>().ToAsyncEnumerable();
                return View(address);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Save( ShippingType shipping, PaymentType payment,string Address)
        {
            var customer= await usermanager.FindByNameAsync(User.Identity.Name);
            string[] ad = Address.Split(' ');
         
            var cart = await cartrepo.Find(customer.Id);
            var totalpricecart = cart.cartitem.Sum(c => c.productitem.price * c.quantity);
            var order = new Order
            {
                customer = customer,
                orderdate = DateTime.UtcNow,
                paymenttypes = payment,
                shippingtypes = shipping,
                totalprice = totalpricecart,
                state=OrderState.unpaid,
                address=Address
            };
           
            switch (shipping)
            {
                case ShippingType.Tipax:
                    order.totalprice = order.totalprice + 10000;
                    break;
                case ShippingType.Post:
                    order.totalprice = order.totalprice + 15000;
                    break;
                default:
                    break;
            }
           
            await orderrepo.Add(order);
            await orderrepo.Save();
            var orderitem = new List<OrderItem>();
            foreach (var item in cart.cartitem) 
            {
                orderitem.Add(new OrderItem
                {
                    order = order,
                    price = item.productitem.price,
                    productitem = item.productitem,
                    quantity = item.quantity
                });
               
            }
            await orderrepo.Add(orderitem);
            await orderrepo.Save();
            var pitem = new List<ProductItem>();
            foreach (var itemm in cart.cartitem)
            {
                pitem.Add(new ProductItem
                {
                    quantity = (byte)itemm.quantity,
                    id = itemm.productitem.id,
                    sellerid= itemm.productitem.sellerid
                });
            }
            await pitemrepo.Update(pitem);
            await pitemrepo.SaveAsync();
            //------------------deletecart---------
            cartrepo.Deleteitems(cart.cartitem);
            await cartrepo.Save();
            await cartrepo.DeleteCart(cart.id);
            await cartrepo.Save();
            //TempData["address"] = address;
            return new RedirectResult($"/Order/Payment/{order.id}");
        }
        /// <summary>
        /// شناسه سفارش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
           public async Task<IActionResult> Payment(int id)
           {
            var order = await orderrepo.Find(id);
            return View(order);
           }
           public async Task<IActionResult> SavePayment(int id, string serial, string date)
            {
            await orderrepo.Update(id, serial, date);
            await orderrepo.Save();
            return new RedirectResult("/Order/Payment/" + id);
            }
       
    }
}
