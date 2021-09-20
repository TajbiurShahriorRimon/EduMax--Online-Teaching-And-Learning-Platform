using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class SalesRecord
    {
        public int SalesRecordId { get; set; }
        public double Amount { get; set; }

        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Required]
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}