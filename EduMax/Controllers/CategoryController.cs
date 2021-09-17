using EduMax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Repository;
using EduMax.Models.ViewModel;

namespace EduMax.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryRepository categoryRepository = new CategoryRepository();

        // GET: Category
        public ActionResult Index()
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

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
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

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

        public ActionResult CategoryByCourse()
        {
            List<ChartModel> dataPoints = new List<ChartModel>();

            List<GraphViewModel> list = this.categoryRepository.NumberOfCourseInCategory();

            foreach(GraphViewModel data in list)
            {
                dataPoints.Add(new ChartModel(data.X_Axis, data.Y_Axis));
            }

            ViewBag.DataPoints = Newtonsoft.Json.JsonConvert.SerializeObject(dataPoints);

            return View("CategoryByCourseChart");
        }

        public JsonResult CategoryByCourse123()
        {
            var list = this.categoryRepository.NumberOfCourseInCategory();
            //return Json(list, JsonRequestBehavior.AllowGet);
            return Json("Test Message", JsonRequestBehavior.AllowGet);
        }
    }
}