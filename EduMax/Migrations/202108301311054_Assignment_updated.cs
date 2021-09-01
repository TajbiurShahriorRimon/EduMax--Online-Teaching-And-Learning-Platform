namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Assignment_updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "FileLocation", c => c.String());
            DropColumn("dbo.Assignments", "FileLication");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignments", "FileLication", c => c.String());
            DropColumn("dbo.Assignments", "FileLocation");
        }
    }
}
