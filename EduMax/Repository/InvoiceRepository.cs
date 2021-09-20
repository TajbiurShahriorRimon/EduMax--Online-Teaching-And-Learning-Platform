using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduMax.Repository;
using EduMax.Models;

namespace EduMax.Repository
{
    public class InvoiceRepository : Repository<Invoice>
    {
        public int GetLatestId()
        {
            string query = "select top 1 * from Invoices order by InvoiceId desc";
            Invoice invoice = context.Invoices.SqlQuery(query).SingleOrDefault();

            return invoice.InvoiceId;
        }
    }
}