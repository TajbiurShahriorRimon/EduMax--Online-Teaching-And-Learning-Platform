using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Models;
using EduMax.Repository;

namespace EduMax.Controllers
{
    public class UserController : Controller
    {
        // GET: Teacher
        //by default it is assigned null. If any value is passes then value will be assgined to the parameter
        public ActionResult Index(string searchCourse = null)
        {
            /*Checks if the parameter consists a value or not.*/
            if(searchCourse != null)
            {
                /*When a user searches for a course, the value will be stored in the viewbag which is used to pass data to the
                 search bar and which wil be displayed*/
                ViewBag.courseSearchQuery = searchCourse;
                /*In the same page the course will be loaded when user searches for a course.*/
                return View("Test", new CourseRepository().SearchCourseByString(searchCourse));
            }
            //The following line takes to the default view of course list. That is the "home page"
            return View("Test", new CourseRepository().GetAll());
        }

        public ActionResult Create()
        {
            User user = new User();
            user.Name = TempData["tempName"].ToString();
            user.Date = DateTime.Now;
            user.Status = "Active";
            user.Institution = TempData["tempInstitution"].ToString();

            Credential credential = new Credential();
            credential.Email = TempData["tempEmail"].ToString();
            credential.Password = TempData["tempPassword"].ToString();
            credential.UserType = "Teacher";

            new UserRepository().Insert(user);
            new CredentialRepository().Insert(credential);
            

            return Content("Created");

        }
    }
}