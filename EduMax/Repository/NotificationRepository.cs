using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduMax.Models;

namespace EduMax.Repository
{
    public class NotificationRepository : Repository<Notification>
    {
        public int GetLatestId()
        {
            string query = @"select top 1 * from Notifications order by NotificationId desc";

            Notification notification = context.Notifications.SqlQuery(query).FirstOrDefault();

            return notification.NotificationId;
        }
    }
}