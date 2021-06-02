using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class Lecture
    {
        public int LectureId { get; set; }
        public string LectureName { get; set; }
        public DateTime Date { get; set; }
        public string CoursePhoto { get; set; }
        public string Status { get; set; }
    }
}