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
        public ActionResult Login(Credential credential)
        {
            Credential user = credentialRepository.CheckForLogin(credential);

            if(user != null)
            {
                if(user.UserType == "Teacher")
                {
                    Session["user_email"] = user.Email.ToString();
                    Session["user_type"] = user.UserType.ToString();
                    return RedirectToAction("Index", "Teacher");
                }
            }

            return View();
        }
    }
}