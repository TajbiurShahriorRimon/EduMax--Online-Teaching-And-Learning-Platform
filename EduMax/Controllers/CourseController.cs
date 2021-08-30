using System;
using System.Collections.Generic;
using System.IO;
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
            /*If no session is set the user will be redirected to the log-in page*/
            if(Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Course course = new Course();

            CategoryRepository categoryList = new CategoryRepository();
            ViewData["categoryList"] = categoryList.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            try
            {
                string fileExtension = Path.GetExtension(course.CoursePic.FileName);               
                string concatFileName = DateTime.Now.ToString() + Session["credential_id"].ToString() + "_" + fileExtension;
                string filePath = Server.MapPath("~/Files/CourseImage/");
                //string fileName = Path.GetFileName(concatFileName);
                string fileName = Path.GetFileName(course.CoursePic.FileName);

                //string fullFilePath = Path.Combine(filePath, concatFileName);
                string fullFilePath = Path.Combine(filePath, fileName);
                course.CoursePic.SaveAs(fullFilePath);

                course.CoursePhoto = "~/Files/CourseImage/" + course.CoursePic.FileName;
                //course.CoursePhoto = fullFilePath;
            }
            catch (Exception exception) { }

            course.Date = DateTime.Now;
            course.UserId = (int)Session["credential_id"];
            course.Status = "1";

            new CourseRepository().Insert(course);

            return RedirectToAction("Index", "User");
        }
    }
}