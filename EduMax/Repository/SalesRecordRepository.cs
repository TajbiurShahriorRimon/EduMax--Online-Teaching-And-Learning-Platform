using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduMax.Models;

namespace EduMax.Repository
{
    public class SalesRecordRepository : Repository<SalesRecord>
    {
        public List<SalesRecord> DetailsOfInvoice(int id)
        {
            string query = $"select * from SalesRecords where InvoiceId = {id}";
            List<SalesRecord> salesRecords = context.Database.SqlQuery<SalesRecord>(query).ToList();

            return salesRecords;
        }

        public List<SalesRecord> SalesRecordList()
        {
            string query = "select * from SalesRecords";
            List<SalesRecord> salesRecords = context.Database.SqlQuery<SalesRecord>(query).ToList();

            return salesRecords;
        }
    }
}