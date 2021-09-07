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
        public UserRepository userRepository = new UserRepository();
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
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
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

        public ActionResult AllUsers()
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View("UserList", new UserRepository().GetAll());
        }
        
        public ActionResult UserDetails(int id)
        {
            return View("UserDetails", this.userRepository.Get(id));
        }

        public ActionResult ChangeStatus(int id)
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }            

            /*Finding the user using the user id*/
            User user = this.userRepository.Get(id);

            /*After Finding the user, the user status is checked. If status is "0" which is "Inactive", User Status will become
             "Active" which is "1", after clicking the button to change user status*/
            if (user.Status == "0")
            {
                //User status is set to "1", which is "Active" 
                user.Status = "1";
                userRepository.Update(user);
            }
            //Else if status is "1" which is "Active", User Status will become
            //"Inactive" which is "0", after clicking the button to change user status
            else
            {
                //User status is set to "0", which is "Inactive" 
                user.Status = "0";
                userRepository.Update(user);
            }
            //After changing the user status it will be redirected to the same page.
            return RedirectToAction("UserDetails", new { id = id });
        }

        public ActionResult EditProfile()
        {
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            int id = (int)Session["credential_id"];
            User user = this.userRepository.Get(id);//Getting the user object by id and assigning to another object as a reference.
            return View(user);
        }

        [HttpPost]
        public ActionResult EditProfile(User user)
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            /*After submitting the form both back-end and front-end validation is done. The follwing line is for back-end validation.*/
            if (ModelState.IsValid)
            {
                //Since User and Credential table have one-to-one relationship between them, therfore both table have to be updated.
                //First the user table will be updated.
                user.Credential.CredentialId = user.UserId;
                this.userRepository.Update(user);

                //The credential table is updated
                Credential credential = new Credential();
                credential.CredentialId = user.Credential.CredentialId;
                credential.Email = user.Credential.Email;
                credential.Password = user.Credential.Password;
                //In Credential table the usertype attribute value is defined by the session
                credential.UserType = Session["user_type"].ToString();

                CredentialRepository credentialRepository = new CredentialRepository();
                credentialRepository.Update(credential);

                return RedirectToAction("Index");
            }   
            //In back-end validation, if validation is not done, then the page will be loaded again
            return View(user);
        }
    }
}