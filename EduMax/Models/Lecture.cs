using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class Lecture
    {
        public int LectureId { get; set; }
        [Required, MaxLength(150)]
        public string LectureName { get; set; }
        public DateTime Date { get; set; }
        public string FileLocation { get; set; }
        public string Status { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }

        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}