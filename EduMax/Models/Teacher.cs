using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class Teacher
    {
        [ForeignKey("Credential")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        [Required]
        public string  Name { get; set; }
        public DateTime Date { get; set; }
        public string Institution { get; set; }
        public string Status { get; set; }

        public virtual Credential Credential { get; set; }

        public List<Course> Courses { get; set; }
    }
}