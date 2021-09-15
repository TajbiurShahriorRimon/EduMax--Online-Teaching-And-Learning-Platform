using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required, MaxLength(100)]
        public string CategoryName { get; set; }
        [Required]
        public string Status { get; set; }

        public virtual List<Course> Courses { get; set; }
    }
}