using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class Course
    {
        //For Many-to-Many relationship this constructor is required.
        public Course()
        {
            this.Students = new HashSet<Student>();
        }

        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
        public DateTime Date { get; set; }
        public string CoursePhoto { get; set; }
        public string Status { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public virtual List<Lecture> Lectures { get; set; }
        public virtual List<Assignment> Assignments { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        [NotMapped]
        public HttpPostedFileBase CoursePic { get; set; }
    }
}