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
        public DateTime Date { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}