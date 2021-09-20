using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<SalesRecord> SalesRecords { get; set; }
    }
}