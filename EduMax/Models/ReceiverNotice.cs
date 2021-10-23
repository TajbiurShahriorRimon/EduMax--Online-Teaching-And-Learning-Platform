using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class ReceiverNotice
    {
        public int ReceiverNoticeId { get; set; }
        public string ReadStatus { get; set; }

        public int NotificationId { get; set; }
        public virtual Notification Notification { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}