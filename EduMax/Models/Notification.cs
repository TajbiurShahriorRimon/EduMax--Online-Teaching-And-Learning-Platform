using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        [Required, MaxLength (150, ErrorMessage = "Maximum 150 characters")]
        public string Title { get; set; }

        [Required, StringLength(int.MaxValue)]
        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}