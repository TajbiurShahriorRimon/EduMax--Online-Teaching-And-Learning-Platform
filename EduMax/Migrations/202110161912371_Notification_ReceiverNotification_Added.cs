namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notification_ReceiverNotification_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Message = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId);
            
            CreateTable(
                "dbo.ReceiverNotices",
                c => new
                    {
                        ReceiverNoticeId = c.Int(nullable: false, identity: true),
                        ReadStatus = c.String(),
                        NotificationId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReceiverNoticeId)
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.NotificationId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceiverNotices", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReceiverNotices", "NotificationId", "dbo.Notifications");
            DropIndex("dbo.ReceiverNotices", new[] { "UserId" });
            DropIndex("dbo.ReceiverNotices", new[] { "NotificationId" });
            DropTable("dbo.ReceiverNotices");
            DropTable("dbo.Notifications");
        }
    }
}
