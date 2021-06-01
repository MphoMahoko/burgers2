using burgers2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace burgers2.Controllers
{
    public class MessagesController : Controller
    {
        ApplicationDbContext _context;

        public MessagesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Messages
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var messages = _context.Messages.ToList();
            return View(messages);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Message message)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                message.Date = DateTime.Now;
                _context.Messages.Add(message);
                _context.SaveChanges();

                TempData["Alert"] = "Message sent";
                return RedirectToAction("Menu", "Meals");
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Read(int id)
        {
            var message = _context.Messages.SingleOrDefault(m=>m.Id == id);
            return View(message);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Respond(string comment, string email, string subject)
        {

            var mail = new MailMessage();
            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress("fightclub244@gmail.com");
            mail.Subject = subject;
            mail.Body = comment;

            //adjust credentials when site is live
            /*
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("fightclub244@gmail.com", "Keabetswe2"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            smtp.Send(mail);
            */

            return RedirectToAction("Index");
        }
    }
}