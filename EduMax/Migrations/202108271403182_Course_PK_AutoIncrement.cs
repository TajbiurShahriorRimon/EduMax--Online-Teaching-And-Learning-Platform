namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Course_PK_AutoIncrement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "UserId" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Users", "UserId");
            CreateIndex("dbo.Users", "UserId");
            AddForeignKey("dbo.Courses", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "UserId" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Users", "UserId");
            CreateIndex("dbo.Users", "UserId");
            AddForeignKey("dbo.Courses", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
