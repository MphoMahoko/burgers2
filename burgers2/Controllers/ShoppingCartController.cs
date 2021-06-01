using burgers2.Models;
using burgers2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace burgers2.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext _context;

        public ShoppingCartController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cartId = getCartId();

            var items = _context.CartItems.Where(c => c.CartId == cartId).Include(c=>c.Meal).ToList();
            var total = 0;

            foreach (var item in items)
            {
                total += ((int)item.Meal.Price * item.Count);
            }

            var cart = new CartViewModel
            {
                CartItems = items, 
                Total = total
            };

            

            return View(cart);
        }

        
        public ActionResult AddToCart(int Id)
        {
            
            if (User.IsInRole("admin"))
            {
                return Content("go back");                
            }
            var meal = _context.Meals.Single(m => m.Id == Id);
            var cartId = getCartId();

            var cartItem = _context.CartItems.SingleOrDefault(c=>c.MealId == meal.Id && c.CartId == cartId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Meal = meal,
                    Count = int.Parse( Request.Form["count"]),
                    CartId = getCartId()
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
            
        }

        public ActionResult MinusCount(int id)
        {
            var cartItem = _context.CartItems.SingleOrDefault(c => c.Id == id);

            if (cartItem.Count > 1)
            {
                cartItem.Count--;
            }
            else
            {
                _context.CartItems.Remove(cartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");

         }

        public ActionResult PlusCount(int id)
        {


            var cartItem = _context.CartItems.SingleOrDefault(c => c.Id == id);

            cartItem.Count++;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Remove(int Id)
        {
            var cartItem = _context.CartItems.Single(c => c.Id == Id);
                       
            _context.CartItems.Remove(cartItem);
           
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // private string getCartId(HttpContextBase httpContext)
        private string getCartId()
        {
            var cartId = string.Empty;

            var cookie =Request.Cookies["ShoppingCart"];

            if (cookie == null)
            {
                cookie = new HttpCookie("ShoppingCart");
                cartId = Guid.NewGuid().ToString();

                cookie.Value = cartId;
                HttpContext.Response.Cookies.Set(cookie);
            }
            else
            {
                cartId = cookie.Value;
            }

            return cartId;
        }
    }
}