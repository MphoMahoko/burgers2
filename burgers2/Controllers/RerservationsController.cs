using burgers2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet;
using Microsoft.AspNet.Identity;

namespace burgers2.Controllers
{
    public class ReservationsController : Controller            
    {
        ApplicationDbContext _context;

         
        public ReservationsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Rerservations
        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            var reservations = _context.Reservations.ToList();

            return View(reservations);
        }


        [Authorize]
        public ActionResult Reserve()
        {
            if (User.IsInRole("admin"))
            {
                return Content("go back");
            }

            ViewBag.Title = "hololo";
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Reserve(Reservation reservation)
        {
            if (User.IsInRole("admin"))
            {
                return Content("go back");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var customerId = User.Identity.GetUserId();
            var customer = _context.Users.Single(a=>a.Id == customerId);


            reservation.Customer = customer;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();


            TempData["Alert"] = "Table succesfully reserved";
            return RedirectToAction("Menu", "Meals");
        }

    
    }
}