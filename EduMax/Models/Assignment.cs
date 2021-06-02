using System;
using System.Collections.Generic;
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
    }
}