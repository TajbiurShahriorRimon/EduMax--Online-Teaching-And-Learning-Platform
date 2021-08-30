namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentCourse_Updated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "StudentCourse_StudentCourseId", "dbo.StudentCourses");
            DropForeignKey("dbo.Teachers", "StudentCourse_StudentCourseId", "dbo.StudentCourses");
            DropIndex("dbo.Courses", new[] { "StudentCourse_StudentCourseId" });
            DropIndex("dbo.Teachers", new[] { "StudentCourse_StudentCourseId" });
            AddColumn("dbo.StudentCourses", "CourseId", c => c.Int(nullable: false));
            AddColumn("dbo.StudentCourses", "TeacherId", c => c.Int(nullable: false));
            DropColumn("dbo.Courses", "StudentCourse_StudentCourseId");
            DropColumn("dbo.Teachers", "StudentCourse_StudentCourseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "StudentCourse_StudentCourseId", c => c.Int());
            AddColumn("dbo.Courses", "StudentCourse_StudentCourseId", c => c.Int());
            DropColumn("dbo.StudentCourses", "TeacherId");
            DropColumn("dbo.StudentCourses", "CourseId");
            CreateIndex("dbo.Teachers", "StudentCourse_StudentCourseId");
            CreateIndex("dbo.Courses", "StudentCourse_StudentCourseId");
            AddForeignKey("dbo.Teachers", "StudentCourse_StudentCourseId", "dbo.StudentCourses", "StudentCourseId");
            AddForeignKey("dbo.Courses", "StudentCourse_StudentCourseId", "dbo.StudentCourses", "StudentCourseId");
        }
    }
}
