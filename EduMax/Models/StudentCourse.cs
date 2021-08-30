using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduMax.Models
{
    public class StudentCourse
    {
        public int StudentCourseId { get; set; }
        public DateTime CourseTakenDate { get; set; }

        public int CourseId { get; set; }
        public int UserStudentId { get; set; }
    }
}