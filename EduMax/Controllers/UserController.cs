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
                return View("Test", new CourseRepository().SearchCourseByString(searchCourse));
            }
            //The following line takes to the default view of course list. That is the "home page"
            return View("Test", new CourseRepository().GetAll());
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
        
        //Following line executes when the User Details view will be shown.
        public ActionResult UserDetails(int id)
        {
            //Getting the user id and assigning to the ViewBag.
            //This user id is needed because when the view gets loaded using JQuery, the id is required to make a jquery ajax request
            //for url: "/User/UserDetailsData/@ViewBag.userIdForDetails"
            ViewBag.userIdForDetails = id;

            //Returning a view with user data by the help if user id. This is because after the end of jquery ajax request, data will
            //be loaded from this data. From this data, only the status will not be displayed. The status data will be takes
            //after the jquery ajax request with the url: "/User/ChangeStatus/" + id
            return View("UserDetails", this.userRepository.Get(id));
        }

        //The following method will be call using jquery ajax request. Only the user status data will be passed
        public ActionResult UserDetailsData(int id)
        {    
            //Getting the user id from the user table with the help of id. Only the user status will be taken from this object
            User user = this.userRepository.Get(id);

            //Returning Json with two parameters, user status and JsonRequestBehavior.AllowGet. This will be returned to the same page
            //where it was called from.
            //That means we are passing user.Status in json format.
            //Take a note, If we want to pass the whole user object, then a runtime error occurs, since User has other associated class.
            return Json(user.Status, JsonRequestBehavior.AllowGet);
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

            //Now after changing and udpdating the user status, we will now retrieve the same user with the help if user id
            User user3 = new UserRepository().Get(id);
            User user2 = new User();

            //user2 status will get assigned from user3.status.
            user2.Status = user3.Status;
            //This will be returned to the same page where it was called from.
            return Json(user2, JsonRequestBehavior.AllowGet);
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