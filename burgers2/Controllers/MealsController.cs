using burgers2.Models;
using burgers2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace burgers2.Controllers
{
    public class MealsController : Controller
    {
        ApplicationDbContext _context;

        public MealsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Meals
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Menu()
        {
           
            var meals = _context.Meals.Include(m=>m.Category).ToList();
            return View(meals);
        }

        [Authorize(Roles ="admin")]
        public ActionResult Edit(int Id)
        {
            var meal = _context.Meals.Single(m => m.Id == Id);


            var mealViewModel = new MealViewModel {
                Categories = _context.Categories.ToList(), 
                CategoryId = meal.Category.Id,
                Description = meal.Description, 
                Name = meal.Name, 
                pictureUrl = meal.pictureUrl, 
                Price = meal.Price
            };

            TempData["Alert"] = "Meal edited";
            return View("Edit",mealViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int Id)
        {
            
            var meal = _context.Meals.Single(m => m.Id == Id);
            meal.Deleted = true;
            _context.SaveChanges();
            

            var meals = _context.Meals.Include(m=>m.Category).ToList();

            TempData["Alert"] = "Meal succesfully deleted";
            return View("Menu", meals);


           
        }

        [Authorize(Roles = "admin")]
        public ActionResult Undelete(int Id)
        {
            var meal = _context.Meals.Single(m => m.Id == Id);
            meal.Deleted = false;
            _context.SaveChanges();

            var meals = _context.Meals.Include(m => m.Category).ToList();

            TempData["Alert"] = "Meal restored";
            return View("Menu", meals);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Add()
        {
           var categories = _context.Categories.ToList();

            var mealViewModel = new MealViewModel {
                Categories = categories
            };

            return View(mealViewModel);
        }

        public ActionResult Single(int id)
        {
            var meal = _context.Meals.Include(m=>m.Category).Single(m => m.Id == id);

            return View(meal);
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Update(HttpPostedFileBase pictureUrl, MealViewModel mealViewModel)
        {
            var categories = _context.Categories.ToList();
            var category = _context.Categories.Single(c => c.Id == mealViewModel.CategoryId);

            if (pictureUrl == null)
            {
                mealViewModel.Categories = categories;
     
                return View("Edit",mealViewModel);
            }

            var picname = DateTime.Now.ToString("ddMMyyyHHmmss") + Path.GetFileName(pictureUrl.FileName);
            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"),  picname);

            
            if (ModelState.IsValid)
            {
                var meal = _context.Meals.Single(m => m.Id == mealViewModel.Id);

                meal.Description = mealViewModel.Description;
                meal.Name = mealViewModel.Name;
                meal.pictureUrl = picname;
                meal.Price = mealViewModel.Price;
                meal.Category.Id = mealViewModel.CategoryId;
               
               
                _context.SaveChanges();

                pictureUrl.SaveAs(_path);

                var meals = _context.Meals.ToList();

                return View("Menu", meals);
            }

            else
            {
                mealViewModel.Categories = categories;
                return View("Add", mealViewModel);          
            }
            
        }

        [Authorize(Roles = "admin")]
        public ActionResult Insert(HttpPostedFileBase pictureUrl, MealViewModel mealViewModel)
        {
            var categories = _context.Categories.ToList();
            var category = _context.Categories.Single(c => c.Id == mealViewModel.CategoryId);

            if (pictureUrl == null)
            {
                mealViewModel.Categories = categories;

                return View("Add", mealViewModel);
            }
            var picname = DateTime.Now.ToString("ddMMyyyHHmmss") + Path.GetFileName(pictureUrl.FileName);

            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), picname);


            if (ModelState.IsValid)
            {
                var meal = new Meal
                {
                    Category = category,
                    Description = mealViewModel.Description,
                    Name = mealViewModel.Name,
                    Price = mealViewModel.Price,
                    pictureUrl = picname
                };

                _context.Meals.Add(meal);
                _context.SaveChanges();

                pictureUrl.SaveAs(_path);

                var meals = _context.Meals.ToList();
                TempData["Alert"] = "Meal added";
                return View("Menu", meals);
            }

            else
            {
                mealViewModel.Categories = categories;
                return View("Add", mealViewModel);
            }

        }
    }
}