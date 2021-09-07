using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Models;
using EduMax.Repository;

namespace EduMax.Controllers
{
    public class CredentialController : Controller
    {
        protected CredentialRepository credentialRepository = new CredentialRepository();

        // GET: Credential
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            if (formCollection["student"] == "Register as Student")
            {
                return Content("fdsfesf");
            }
            else if(formCollection["teacher"] == "Register as Teacher")
            {
                if(formCollection["Name"] == "")
                {
                    ViewData["EmailValue"] = formCollection["Email"];
                    ViewData["PasswordValue"] = formCollection["Password"];
                    ViewData["errNameMsg"] = "Name Cannot be empty";

                    return View();
                }
                if(formCollection["Email"] == "")
                {
                    ViewData["NameValue"] = formCollection["Name"];
                    ViewData["PasswordValue"] = formCollection["password"];
                    ViewData["errEmailMsg"] = "Email Cannot be empty";  
                    
                    return View();
                }
                if(formCollection["password"] == "")
                {
                    ViewData["NameValue"] = formCollection["Name"];
                    ViewData["EmailValue"] = formCollection["Email"];
                    ViewData["errPasswordMsg"] = "Password Cannot be empty";

                    return View();
                }
                TempData["tempName"] = formCollection["Name"];
                TempData["tempEmail"] = formCollection["Email"];
                TempData["tempPassword"] = formCollection["password"];
                TempData["tempInstitution"] = formCollection["Institution"];
                return RedirectToAction("Create", "Teacher");
            }
            return Content("registered");
        }

        [HttpPost]
        public ActionResult Login(Credential credential)
        {
            Credential user = credentialRepository.CheckForLogin(credential);

            if(user != null)
            {
                //When user log in, the session value is assigned.
                Session["user_email"] = user.Email.ToString();
                Session["user_type"] = user.UserType.ToString();
                Session["credential_id"] = user.CredentialId;

                if (user.UserType == "Teacher" || user.UserType == "Admin")
                {
                    return RedirectToAction("Index", "User");
                }
            }

            return View();
        }        
    }
}