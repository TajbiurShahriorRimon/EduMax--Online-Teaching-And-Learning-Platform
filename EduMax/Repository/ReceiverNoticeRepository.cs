using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduMax.Models;
using System.Data.Sql;

namespace EduMax.Repository
{
    public class ReceiverNoticeRepository : Repository<ReceiverNotice>
    {
        public ReceiverNotice GetReceiverNoticeInfo(int id)//Notification ID
        {
            /*Select * from ReceiverNotices where NotificationId = id;*/
            return new ReceiverNoticeRepository().GetAll().Where(x => x.NotificationId == id).FirstOrDefault();
        }

        public void ChangeReaderStatus(ReceiverNotice receiverNotice)
        {            
            this.Update(receiverNotice);
        }
    }
}