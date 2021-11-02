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
        ReceiverNoticeRepository receiverNoticeRepository = new ReceiverNoticeRepository();

        //Notification list for the particular user
        public ActionResult Index()
        {
            /*If no session for Login is set the user will be redirected to the landing page*/
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

            return View(receiverNotices);
        }

        //The following function executes when user clicks on a particular notice.
        public ActionResult Get(int id) //NotificationID
        {
            /*If no session for Login is set the user will be redirected to the landing page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ReceiverNotice receiverNotice = new ReceiverNotice();
            //Getting the information by the help of NotificationId (which is not primary key) from RecieverNotices 
            receiverNotice = this.receiverNoticeRepository.GetReceiverNoticeInfo(id);
            
            //Now the following lines must be executed by creating a new instance of ReceiverNotice and then assigning the values
            //to its properties one-by-one.
            ReceiverNotice receiverNoticeInfo = new ReceiverNotice();
            receiverNoticeInfo.ReceiverNoticeId = receiverNotice.ReceiverNoticeId;
            receiverNoticeInfo.NotificationId = receiverNotice.NotificationId;
            receiverNoticeInfo.UserId = receiverNotice.UserId;
            //Now changing the ReadStatus to "1", meaning user has clicked on the notice.
            receiverNoticeInfo.ReadStatus = "1";

            //Without the above steps, if we want to update it directly without assiging values to the properties, then
            // a run-time exception will occur.

            //Now updating the ReceiverNotice table.
            new ReceiverNoticeRepository().ChangeReaderStatus(receiverNoticeInfo);

            return View(new NotificationRepository().Get(id));
        }

        public ActionResult Create(int id)
        {
            /*If no session for Login is set the user will be redirected to the landing page*/
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
            /*If no session for Login is set the user will be redirected to the landing page*/
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