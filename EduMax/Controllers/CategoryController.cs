using EduMax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Repository;

namespace EduMax.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryRepository categoryRepository = new CategoryRepository();

        // GET: Category
        public ActionResult Index()
        {
            EduMaxDbContext dbContext = new EduMaxDbContext();
            List<Category> categories = dbContext.Categories.ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            category.Status = "Active";
            this.categoryRepository.Insert(category);
            return View();
        }

        public ActionResult Edit(int id)
        {
            Category category = this.categoryRepository.Get(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryRepository.Update(category);
            return View();
        }

        [HttpGet]
        public ActionResult CategoryStatus(int id) //For changing the category Status
        {
            Category category = categoryRepository.Get(id);
            if(category.Status == "Active")
            {
                category.Status = "Inactive";
                categoryRepository.Update(category);
            }
            else
            {
                category.Status = "Active";
                categoryRepository.Update(category);
            }
            return RedirectToAction("Index");
        }
    }
}