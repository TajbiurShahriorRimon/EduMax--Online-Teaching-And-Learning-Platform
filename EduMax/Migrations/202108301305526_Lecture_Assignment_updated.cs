namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lecture_Assignment_updated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.Lectures", "Course_CourseId", "dbo.Courses");
            DropIndex("dbo.Assignments", new[] { "Course_CourseId" });
            DropIndex("dbo.Lectures", new[] { "Course_CourseId" });
            RenameColumn(table: "dbo.Assignments", name: "Course_CourseId", newName: "CourseId");
            RenameColumn(table: "dbo.Lectures", name: "Course_CourseId", newName: "CourseId");
            AddColumn("dbo.Assignments", "FileLication", c => c.String());
            AddColumn("dbo.Lectures", "FileLocation", c => c.String());
            AlterColumn("dbo.Assignments", "CourseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Lectures", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Assignments", "CourseId");
            CreateIndex("dbo.Lectures", "CourseId");
            AddForeignKey("dbo.Assignments", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
            AddForeignKey("dbo.Lectures", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
            DropColumn("dbo.Lectures", "CoursePhoto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lectures", "CoursePhoto", c => c.String());
            DropForeignKey("dbo.Lectures", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Assignments", "CourseId", "dbo.Courses");
            DropIndex("dbo.Lectures", new[] { "CourseId" });
            DropIndex("dbo.Assignments", new[] { "CourseId" });
            AlterColumn("dbo.Lectures", "CourseId", c => c.Int());
            AlterColumn("dbo.Assignments", "CourseId", c => c.Int());
            DropColumn("dbo.Lectures", "FileLocation");
            DropColumn("dbo.Assignments", "FileLication");
            RenameColumn(table: "dbo.Lectures", name: "CourseId", newName: "Course_CourseId");
            RenameColumn(table: "dbo.Assignments", name: "CourseId", newName: "Course_CourseId");
            CreateIndex("dbo.Lectures", "Course_CourseId");
            CreateIndex("dbo.Assignments", "Course_CourseId");
            AddForeignKey("dbo.Lectures", "Course_CourseId", "dbo.Courses", "CourseId");
            AddForeignKey("dbo.Assignments", "Course_CourseId", "dbo.Courses", "CourseId");
        }
    }
}
