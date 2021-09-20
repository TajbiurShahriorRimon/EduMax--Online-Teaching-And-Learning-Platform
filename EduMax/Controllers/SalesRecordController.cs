using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Repository;
using EduMax.Models;

namespace EduMax.Controllers
{
    public class SalesRecordController : Controller
    {
        public ActionResult Details(int id)
        {
            ViewBag.Id = id;
            /*List<Invoice> invoices = new List<Invoice>();
            invoices[1].SalesRecords.Cou*/
            //List<SalesRecord> salesRecords = new SalesRecordRepository().DetailsOfInvoice(id);
            return View(new SalesRecordRepository().GetAll());
        }
    }
}