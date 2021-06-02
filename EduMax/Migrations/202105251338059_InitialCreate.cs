namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId)
                .ForeignKey("dbo.Credentials", t => t.AdminId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        CredentialId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(),
                        UserType = c.String(),
                    })
                .PrimaryKey(t => t.CredentialId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Institution = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Credentials", t => t.StudentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CoursePhoto = c.String(),
                        Status = c.String(),
                        Category_CategoryId = c.Int(),
                        Teacher_TeacherId = c.Int(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .ForeignKey("dbo.Teachers", t => t.Teacher_TeacherId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Teacher_TeacherId);
            
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        AssignmentName = c.String(),
                        Marks = c.Single(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Course_CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId)
                .Index(t => t.Course_CourseId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Lectures",
                c => new
                    {
                        LectureId = c.Int(nullable: false, identity: true),
                        LectureName = c.String(),
                        Date = c.DateTime(nullable: false),
                        CoursePhoto = c.String(),
                        Status = c.String(),
                        Course_CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.LectureId)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId)
                .Index(t => t.Course_CourseId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Institution = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.Credentials", t => t.TeacherId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.CourseStudents",
                c => new
                    {
                        Course_CourseId = c.Int(nullable: false),
                        Student_StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Course_CourseId, t.Student_StudentId })
                .ForeignKey("dbo.Courses", t => t.Course_CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_StudentId, cascadeDelete: true)
                .Index(t => t.Course_CourseId)
                .Index(t => t.Student_StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admins", "AdminId", "dbo.Credentials");
            DropForeignKey("dbo.Students", "StudentId", "dbo.Credentials");
            DropForeignKey("dbo.Teachers", "TeacherId", "dbo.Credentials");
            DropForeignKey("dbo.Courses", "Teacher_TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.CourseStudents", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.CourseStudents", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.Lectures", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Assignments", "Course_CourseId", "dbo.Courses");
            DropIndex("dbo.CourseStudents", new[] { "Student_StudentId" });
            DropIndex("dbo.CourseStudents", new[] { "Course_CourseId" });
            DropIndex("dbo.Teachers", new[] { "TeacherId" });
            DropIndex("dbo.Lectures", new[] { "Course_CourseId" });
            DropIndex("dbo.Assignments", new[] { "Course_CourseId" });
            DropIndex("dbo.Courses", new[] { "Teacher_TeacherId" });
            DropIndex("dbo.Courses", new[] { "Category_CategoryId" });
            DropIndex("dbo.Students", new[] { "StudentId" });
            DropIndex("dbo.Admins", new[] { "AdminId" });
            DropTable("dbo.CourseStudents");
            DropTable("dbo.Teachers");
            DropTable("dbo.Lectures");
            DropTable("dbo.Categories");
            DropTable("dbo.Assignments");
            DropTable("dbo.Courses");
            DropTable("dbo.Students");
            DropTable("dbo.Credentials");
            DropTable("dbo.Admins");
        }
    }
}
