using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Repository;
using EduMax.Models;

namespace EduMax.Controllers
{
    public class UserFavoriteCourseController : Controller
    {
        // GET: UserFavoriteCourse
        //Following method is called when user wants to see his/her Favorite courses list.
        public ActionResult Index()
        {
            /*If no session for Login is set the user will be redirected to the landing page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Assigning the user session value
            int userId = (int) Session["credential_id"];

            //Getting the Favorite course list for that particular user.
            List<UserFavoriteCourse> userFavoriteCourses = new UserFavoriteCourseRepository().List(userId);

            //Since we Course data have to be to the view therefore it is required to get Course data.
            List<Course> courses = new List<Course>();

            foreach(UserFavoriteCourse userFavorite in userFavoriteCourses)
            {
                Course course = new Course();
                //Getting the particular course data
                course = new CourseRepository().GetCourseData(userFavorite.CourseId);
                courses.Add(course);
            }
            
            return View(courses);
        }
    }
}