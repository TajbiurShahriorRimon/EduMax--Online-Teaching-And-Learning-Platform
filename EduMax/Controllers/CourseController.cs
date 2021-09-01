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
            /*When uses wants to create a course, the users first have to create the course, but the course will not be created at first.
            Rather it will be stored in a session variable which is done in the the following line*/
            Session["createCourse"] = course;

            /*Then page will be rediected a page which helps creating one or multiple lectures. Following line is used for redierction*/
            return RedirectToAction("Create", "Lecture");
        }

        public ActionResult Insert()
        {
            /*So after the completion of adding lecture to course, the course will be inserted into database*/
            Course course = new Course();
            course = (Course)Session["createCourse"];

            try
            {
                //The course consists file...

                string fileExtension = Path.GetExtension(course.CoursePic.FileName);
                string concatFileName = DateTime.Now.ToString() + Session["credential_id"].ToString() + "_" + fileExtension;
                string filePath = Server.MapPath("~/Files/CourseImage/");
                //string fileName = Path.GetFileName(concatFileName);
                string fileName = Path.GetFileName(course.CoursePic.FileName);

                //string fullFilePath = Path.Combine(filePath, concatFileName);
                string fullFilePath = Path.Combine(filePath, fileName);

                //Following line will save the file to the given path in the server (local machine)
                course.CoursePic.SaveAs(fullFilePath);

                //The path will be saved in the database so that it can retrieve the file in the view when needed.
                course.CoursePhoto = "~/Files/CourseImage/" + course.CoursePic.FileName;
                //course.CoursePhoto = fullFilePath;
            }
            catch (Exception exception) { }

            course.Date = DateTime.Now;
            course.UserId = (int)Session["credential_id"];
            course.Status = "1";

            //Inserting the course into the database
            new CourseRepository().Insert(course);

            //After inserting the course into the database, it is time to store lectures into the database. Therefore it is redirected
            return RedirectToAction("InsertLectures", "Lecture");
        }
    }
}