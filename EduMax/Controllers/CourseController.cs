using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Models;
using EduMax.Repository;

namespace EduMax.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            Course course = new Course();

            CategoryRepository categoryList = new CategoryRepository();
            ViewData["categoryList"] = categoryList.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {

            return RedirectToAction("Index");
        }
    }
}