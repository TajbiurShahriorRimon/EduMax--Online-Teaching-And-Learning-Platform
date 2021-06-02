namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EduMaxDbChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Courses", "Teacher_TeacherId", "dbo.Teachers");
            DropIndex("dbo.Courses", new[] { "Category_CategoryId" });
            DropIndex("dbo.Courses", new[] { "Teacher_TeacherId" });
            RenameColumn(table: "dbo.Courses", name: "Category_CategoryId", newName: "CategoryId");
            RenameColumn(table: "dbo.Courses", name: "Teacher_TeacherId", newName: "TeacherId");
            AlterColumn("dbo.Courses", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "TeacherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "CategoryId");
            CreateIndex("dbo.Courses", "TeacherId");
            AddForeignKey("dbo.Courses", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Courses", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Courses", new[] { "TeacherId" });
            DropIndex("dbo.Courses", new[] { "CategoryId" });
            AlterColumn("dbo.Courses", "TeacherId", c => c.Int());
            AlterColumn("dbo.Courses", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.Courses", name: "TeacherId", newName: "Teacher_TeacherId");
            RenameColumn(table: "dbo.Courses", name: "CategoryId", newName: "Category_CategoryId");
            CreateIndex("dbo.Courses", "Teacher_TeacherId");
            CreateIndex("dbo.Courses", "Category_CategoryId");
            AddForeignKey("dbo.Courses", "Teacher_TeacherId", "dbo.Teachers", "TeacherId");
            AddForeignKey("dbo.Courses", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
