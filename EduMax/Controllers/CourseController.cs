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
        EduMaxDbContext context = new EduMaxDbContext();
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string searchCourse = null)
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            /*Checks if the parameter consists a value or not.*/
            if (searchCourse != null)
            {
                /*When a user searches for a course, the value will be stored in the viewbag which is used to pass data to the
                 search bar and which wil be displayed*/
                ViewBag.courseSearchQuery = searchCourse;
                /*In the same page the course will be loaded when user searches for a course.*/
                return View("List", new CourseRepository().SearchCourseByString(searchCourse));
            }
            //The following line takes to the default view of course list. That is the "home page"
            return View("List", new CourseRepository().GetAll());
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
            /*When user wants to create a course, the users first have to create the course, but the course will not be created at first.
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

        [HttpPost]
        public ActionResult SearchCourse(string searchCourse)
        {
            //When user search for a course using the search bar, first from javascript file it will be checked whether the
            //the search string is empty or not. If it is empty, the js function will return false; else this method wlll be executed
            //and the following line will be exectued.
            return RedirectToAction("List", new{ searchCourse });
        }

        public ActionResult CourseLectureList(int id)
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //Getting the data of particular course id.
            Course course = new CourseRepository().Get(id);

            //After getting the data, we have to check if the user logged-in has the  course for learning.
            StudentCourse studentCourse = new StudentCourseRepository().CheckIfStudentHasCourse((int)Session["credential_id"], course.CourseId);
            //The following line executes, meaning user do not have the course for learning, as a result the Signal is set to false
            if (studentCourse == null)
            {
                ViewBag.Signal = false;
            }
            //Else The following line executes, meaning user has the course for learning, as a result the Signal is set to true
            else
            {
                ViewBag.Signal = true;
            }
            return View(course);
        }

        public ActionResult Cart()
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        public ActionResult UserCreatedCourses()
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Course> courses = new CourseRepository().UserCreatedCourseList((int)Session["credential_id"]);
            return View(courses);
        }

        public ActionResult UserLearningCourses()
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            int id = (int)Session["credential_id"];
            List<Course> courses = new CourseRepository().UserLearningCourseList(id);
            return View(courses);
        }        
    }
}