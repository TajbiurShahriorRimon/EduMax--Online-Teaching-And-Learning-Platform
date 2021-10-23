using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class User
    {
        //Since User table has a one-to-one relationship with Credential table there fore we have to write the following annotation.
        //Here UserId is both a primary key (which is not auto incremented) and a Foreign Key of Credential Table
        [ForeignKey("Credential")]        
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required!"), MaxLength(150), MinLength(3, ErrorMessage = "Must be at least 3 characters")]
        public string  Name { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Institution is required!")]
        public string Institution { get; set; }

        [Required]
        public string Status { get; set; }
        public string NotificationStatus { get; set; }

        public virtual Credential Credential { get; set; }

        public virtual List<Course> Courses { get; set; }
        public virtual List<Invoice> Invoices { get; set; }

    }
}