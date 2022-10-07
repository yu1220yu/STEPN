using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STEPÑ.Data;
using STEPÑ.Helpers;
using STEPÑ.Models;
using System.Collections.Generic;
using System.Linq;
using static STEPÑ.Models.Order;

namespace STEPÑ.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly STEPÑContext _context;

        public CartController(STEPÑContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CartItem> CartItems = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

            if (CartItems != null)
            {
                ViewBag.Total = CartItems.Sum(m => m.Price);
            }
            else
            {
                ViewBag.Total = 0;
            }

            return View(CartItems);
        }
        public IActionResult AddtoCart(int id)
        {
            var product = _context.Product.Single(x => x.Id.Equals(id));
            CartItem item = new CartItem()
            {
                ProductId = product.Id,
                Product = product,
                Price = product.Price,
            };

            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                
                cart.Add(item);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = cart.FindIndex(m => m.Product.Id.Equals(id));

                if (index != -1)
                {
                    cart[index] = item;
                }
                else
                {
                    cart.Add(item);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return NoContent();
        }
        public IActionResult RemoveItem(int id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = cart.FindIndex(m => m.Product.Id.Equals(id));
            
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Index");
        }
    }
}
