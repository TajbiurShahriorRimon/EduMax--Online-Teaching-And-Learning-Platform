namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_table_StudentCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        StudentCourseId = c.Int(nullable: false, identity: true),
                        CourseTakenDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StudentCourseId);
            
            AddColumn("dbo.Courses", "StudentCourse_StudentCourseId", c => c.Int());
            AddColumn("dbo.Teachers", "StudentCourse_StudentCourseId", c => c.Int());
            CreateIndex("dbo.Courses", "StudentCourse_StudentCourseId");
            CreateIndex("dbo.Teachers", "StudentCourse_StudentCourseId");
            AddForeignKey("dbo.Courses", "StudentCourse_StudentCourseId", "dbo.StudentCourses", "StudentCourseId");
            AddForeignKey("dbo.Teachers", "StudentCourse_StudentCourseId", "dbo.StudentCourses", "StudentCourseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "StudentCourse_StudentCourseId", "dbo.StudentCourses");
            DropForeignKey("dbo.Courses", "StudentCourse_StudentCourseId", "dbo.StudentCourses");
            DropIndex("dbo.Teachers", new[] { "StudentCourse_StudentCourseId" });
            DropIndex("dbo.Courses", new[] { "StudentCourse_StudentCourseId" });
            DropColumn("dbo.Teachers", "StudentCourse_StudentCourseId");
            DropColumn("dbo.Courses", "StudentCourse_StudentCourseId");
            DropTable("dbo.StudentCourses");
        }
    }
}
