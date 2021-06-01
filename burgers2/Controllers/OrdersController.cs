using burgers2.Models;
using burgers2.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace burgers2.Controllers
{
    public class OrdersController : Controller
    {
        ApplicationDbContext _context;

        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Orders
        [Authorize]
        public ActionResult Index()
        {
            TempData["userId"] = User.Identity.GetUserId();
            var userid = User.Identity.GetUserId();

            var orders = _context.Orders.Where(o=>o.CustomerId == userid).ToList();

            if (orders.Count == 0)
            {
                ViewBag.order = false;
            }
            else if (User.IsInRole("admin"))
            {
                orders = _context.Orders.ToList();
                ViewBag.order = true;
            }
            else
            {
                ViewBag.order = true;
            }

            return View(orders);

            
        }

        [Authorize]
        public ActionResult Checkout()
        {
            var cartId = getCartId();

            var items = _context.CartItems.Where(c => c.CartId == cartId).Include(c => c.Meal).ToList();
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

            ViewBag.cart = cart;

            return View();

           
        }

        [Authorize]
        [HttpPost]
        public ActionResult Checkout(Order customerorder)
        {
            if (!ModelState.IsValid)
            {
                return View();
 
            }
            
            var customerID = User.Identity.GetUserId();
            var customer = _context.Users.Single(u=>u.Id ==customerID);

            var cartId = GetCartId(HttpContext);
            var cartItems = _context.CartItems.Include(c=>c.Meal).Where(c => c.CartId == cartId).ToList();

            var totalprice = 0;

            var order = new Order {
                Address1 = customerorder.Address1, 
                Address2 = customerorder.Address2, 
                Customer = customer, 
                Email =customerorder.Email, 
                Firstname = customerorder.Firstname, 
                Lastname = customerorder.Lastname, 
                Phone = customerorder.Phone,
                Postalcode = customerorder.Postalcode, 
                Status = "new", 
                Date = DateTime.Now
            };

            foreach (var item in cartItems)
            {
                totalprice += (int)(item.Count * item.Meal.Price);

                var orderDetail = new OrderDetail
                {
                    Meal = item.Meal, 
                    OrderId = order.Id, 
                    Quantity = item.Count,
                    TotalPrice = item.Count * item.Meal.Price
                   
                };

                order.OrderDetails.Add(orderDetail);

                //emptying the cart
                _context.CartItems.Remove(item);
            }

            order.Total = totalprice;
            _context.Orders.Add(order);

            _context.SaveChanges();

            TempData["Alert"] = "Order succesfully placed";
            return RedirectToAction("Orderdetails","Orders", new {id = order.Id });
            
        }

        public string GetCartId(HttpContextBase http)
        {
            var cart = http.Request.Cookies.Get("ShoppingCart");
            var cartId = string.Empty;

            if (cart == null)
            {
                cart = new HttpCookie("ShoppingCart");
                cartId = Guid.NewGuid().ToString();

                cart.Value = cartId;
                http.Response.Cookies.Set(cart);
            }
            else
            {
                cartId = cart.Value;
            }

            return cartId;
        }

        private string getCartId()
        {
            var cartId = string.Empty;

            var cookie = Request.Cookies["ShoppingCart"];

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


        [Authorize(Roles = "admin")]
        public ActionResult Complete(int id)
        {
            var order = _context.Orders.Single(o=>o.Id == id);
            order.Status = "complete";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Process(int id)
        {
            var order = _context.Orders.Single(o => o.Id == id);
            order.Status = "process";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Cancel(int id)
        {
            var order = _context.Orders.Single(o => o.Id == id);
            order.Status = "cancel";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult Orderdetails(int id)
        {
            var order = _context.Orders.Include(o=>o.OrderDetails.Select(c=>c.Meal)).Single(o=>o.Id == id);
            return View(order);
        }
    }
}