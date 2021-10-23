using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Models;
using EduMax.Repository;

namespace EduMax.Controllers
{
    public class NotificationController : Controller
    {
        UserRepository userRepository = new UserRepository();

        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id)
        {
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Assigning the user info to the ViewBag which will be passed in the View.
            ViewBag.UserInfoForNotice = new UserRepository().Get(id);
            return View();
        }

        [HttpPost]
        public ActionResult Create(Notification notification, int userId)
        {
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            /*After submitting the form both back-end and front-end validation is done. The follwing line is for back-end validation.*/
            if (ModelState.IsValid)
            {                
                notification.Date = DateTime.Now; //Assigning the current date & time.

                /*First the data will be inserted into Notification Table*/
                new NotificationRepository().Insert(notification);
                
                ReceiverNotice receiverNotice = new ReceiverNotice();
                //Setting ReadStatus to "0". Meaning This Particular Notification is not red by the user
                receiverNotice.ReadStatus = "0";
                receiverNotice.UserId = userId;
                receiverNotice.NotificationId = new NotificationRepository().GetLatestId();

                // Secondly the data will be inserted into ReceiverNotice table.
                new ReceiverNoticeRepository().Insert(receiverNotice);

                User user = this.userRepository.Get(userId);
                //Assigning NotificationStatus to "0". Meaning the Notification button is not clicked by the user after getting a new notice
                user.NotificationStatus = "0";

                // Finally the data will be inserted into User table.
                this.userRepository.Update(user);

                //After all the successful insertion, the system redirect to the same page
                return RedirectToAction("Create", new { userId });
            }

            //In back-end validation, if validation is not done, then the page will be loaded again
            //Againg the user information is taken and assigned it to ViewBag.
            ViewBag.UserInfoForNotice = new UserRepository().Get(userId);
            return View(notification);
        }
    }
}