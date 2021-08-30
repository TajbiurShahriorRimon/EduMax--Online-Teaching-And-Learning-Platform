namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Course_Updated2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "User_UserId", "dbo.Users");
            DropIndex("dbo.Courses", new[] { "User_UserId" });
            RenameColumn(table: "dbo.Courses", name: "User_UserId", newName: "UserId");
            AlterColumn("dbo.Courses", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "UserId");
            AddForeignKey("dbo.Courses", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "UserId", "dbo.Users");
            DropIndex("dbo.Courses", new[] { "UserId" });
            AlterColumn("dbo.Courses", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Courses", name: "UserId", newName: "User_UserId");
            CreateIndex("dbo.Courses", "User_UserId");
            AddForeignKey("dbo.Courses", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
