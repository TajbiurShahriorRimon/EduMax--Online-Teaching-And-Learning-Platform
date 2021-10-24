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

        //Notification list for the particular user
        public ActionResult Index()
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //The user credential id is reauired in order to get the user notification list.
            // converting the credential id which is in the session and assigning it to a variable.
            int userId = Convert.ToInt32(Session["credential_id"]);

            //Now the data is retrieved with the help of lambda expression.
            /*From the ReceiverNotices table, All the data are retrived where the user id matches the credential id*/
            List<ReceiverNotice> receiverNotices = new ReceiverNoticeRepository().GetAll().Where(x => x.UserId == userId).ToList();

            List<Notification> notifications = new List<Notification>();
            
            /*Now the notice data is required to retrieve from the Notifications Table*/
            for(int i = 0; i < receiverNotices.Count; i++)
            {
                Notification notice = new Notification();
                //Getting the notice list from the Notifications table only the particular notification id data found
                //from the ReceiverNotifications table.
                notice = new NotificationRepository().Get(receiverNotices[i].NotificationId);

                //Adding the data into the generic list.
                notifications.Add(notice);
            }
            return View(notifications);
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