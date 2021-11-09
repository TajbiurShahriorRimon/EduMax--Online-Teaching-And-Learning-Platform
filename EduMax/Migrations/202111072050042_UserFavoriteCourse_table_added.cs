namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFavoriteCourse_table_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFavoriteCourses",
                c => new
                    {
                        UserFavoriteCourseId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserFavoriteCourseId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFavoriteCourses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserFavoriteCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.UserFavoriteCourses", new[] { "CourseId" });
            DropIndex("dbo.UserFavoriteCourses", new[] { "UserId" });
            DropTable("dbo.UserFavoriteCourses");
        }
    }
}
