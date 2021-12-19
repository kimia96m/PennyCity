using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Products.Carts;
using WebApplication2.Models.Products.ProductItems;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    public class CartController : Controller
    {
        private UserManager<Operator> usermanager;
        private ICartRepository cartrepo;
        private IProductItemRepository productitemrepo;
        public CartController(UserManager<Operator> usermanager, ICartRepository cartrepo, IProductItemRepository productitemrepo)
        {
            this.productitemrepo = productitemrepo;
            this.usermanager = usermanager;
            this.cartrepo = cartrepo;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(int? productitemId, int? count)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Acount");
            }
            else
            {
                var user = await usermanager.FindByNameAsync(User.Identity.Name);
                var cart = await cartrepo.Find(user.Id);
                if (productitemId !=null && count != null)
                {
                    var pitem = await productitemrepo.FindAsync((int)productitemId);
                    if (cart != null)
                    {
                        if (cart.cartitem.Any(c => c.productitem.id == productitemId))
                        {
                            var cartitem = cart.cartitem.FirstOrDefault(c => c.productitem.id==productitemId);
                            await cartrepo.Update(new CartItem { id = cartitem.id, quantity = (int)count });
                            await cartrepo.Save();
                            var shopingcartfinal = await cartrepo.Find(user.Id);
                            return View(shopingcartfinal);
                        }
                        else
                        {
                            await cartrepo.Add(new CartItem
                            {
                                cart = cart,
                                productitem = pitem,
                                quantity = (int)count,
                                
                            });
                            await cartrepo.Save();
                            var shopingcartfinal = await cartrepo.Find(user.Id);
                            return View(shopingcartfinal);
                        }
                    }
                  
                    else
                    {
                        var shopcart = new Cart
                        {
                            customer = user
                        };
                        await cartrepo.Add(shopcart);
                        await cartrepo.Save();
                        pitem = await productitemrepo.FindAsync((int)productitemId);
                        var cartfinal = await cartrepo.Find(shopcart.id);
                        await cartrepo.Add(new CartItem { cart = cartfinal, productitem = pitem, quantity = (int)count });
                        await cartrepo.Save();
                        var shopingcartfinal = await cartrepo.Find(user.Id);
                        return View(shopingcartfinal);
                    }

                }
                else
                {
                    return View(cart);
                }


            }
 
        }
        public async Task<IActionResult> Remove( int id)
        {
            await cartrepo.delete(id);
            await cartrepo.Save();
            return new RedirectResult("/Cart");
        }
    }
}
