using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public float Marks { get; set; }
        public DateTime Date { get; set; }
        public string FileLocation { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}