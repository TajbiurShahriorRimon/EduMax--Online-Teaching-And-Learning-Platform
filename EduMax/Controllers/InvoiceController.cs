using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Models;
using EduMax.Repository;

namespace EduMax.Controllers
{
    public class InvoiceController : Controller
    {
        public ActionResult Index()
        {
            List<Invoice> invoices = new InvoiceRepository().GetAll();
            return View(invoices);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            Invoice invoice = new Invoice();
            invoice.TotalAmount = Convert.ToDouble(collection["totalPaymentPrice"]);
            invoice.UserId = (int)Session["credential_id"];
            invoice.Date = DateTime.Now;

            new InvoiceRepository().Insert(invoice);           

            //Assigning the cart items to list of Course.
            List<Course> courses = (List<Course>)Session["shoppingCart"];
            SalesRecord sales = new SalesRecord();
            StudentCourse studentCourse = new StudentCourse();

            int id = new InvoiceRepository().GetLatestId();

            for(int i = 0; i < courses.Count; i++)
            {
                sales.CourseId = courses[i].CourseId;
                sales.Amount = courses[i].Price;
                sales.InvoiceId = id;

                new SalesRecordRepository().Insert(sales);

                studentCourse.CourseId = courses[i].CourseId;
                studentCourse.CourseTakenDate = DateTime.Now;
                studentCourse.UserStudentId = (int)Session["credential_id"];

                new StudentCourseRepository().Insert(studentCourse);
            }

            Session["shoppingCart"] = null;

            return RedirectToAction("Index");
        }
    }
}