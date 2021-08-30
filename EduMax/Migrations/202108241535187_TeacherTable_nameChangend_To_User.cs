namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherTable_nameChangend_To_User : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "TeacherId", "dbo.Credentials");
            DropIndex("dbo.Courses", new[] { "TeacherId" });
            DropIndex("dbo.Teachers", new[] { "TeacherId" });
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Institution = c.String(),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Credentials", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Courses", "User_UserId", c => c.Int());
            AddColumn("dbo.StudentCourses", "UserStudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "User_UserId");
            AddForeignKey("dbo.Courses", "User_UserId", "dbo.Users", "UserId");
            DropColumn("dbo.StudentCourses", "TeacherId");
            DropTable("dbo.Teachers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Institution = c.String(),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId);
            
            AddColumn("dbo.StudentCourses", "TeacherId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Users", "UserId", "dbo.Credentials");
            DropForeignKey("dbo.Courses", "User_UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "UserId" });
            DropIndex("dbo.Courses", new[] { "User_UserId" });
            DropColumn("dbo.StudentCourses", "UserStudentId");
            DropColumn("dbo.Courses", "User_UserId");
            DropTable("dbo.Users");
            CreateIndex("dbo.Teachers", "TeacherId");
            CreateIndex("dbo.Courses", "TeacherId");
            AddForeignKey("dbo.Teachers", "TeacherId", "dbo.Credentials", "CredentialId");
            AddForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
        }
    }
}
