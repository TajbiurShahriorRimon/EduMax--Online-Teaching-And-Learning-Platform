using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class Student
    {
        //For Many-to-Many relationship this constructor is required.
        public Student()
        {
            this.Courses = new HashSet<Course>();
        }

        [ForeignKey("Credential")]
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Institution { get; set; }
        public string Status { get; set; }

        public virtual Credential Credential { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}