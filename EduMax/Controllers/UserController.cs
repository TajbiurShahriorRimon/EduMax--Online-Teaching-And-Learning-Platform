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
        public ActionResult Index()
        {
            //return View(new CourseRepository().GetAll());
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