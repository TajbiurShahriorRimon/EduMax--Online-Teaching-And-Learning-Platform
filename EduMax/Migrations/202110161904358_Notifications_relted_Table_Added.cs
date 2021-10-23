namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notifications_relted_Table_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "NotificationStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "NotificationStatus");
        }
    }
}
