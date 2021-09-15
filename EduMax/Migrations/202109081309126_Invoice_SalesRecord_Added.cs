namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Invoice_SalesRecord_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.SalesRecords",
                c => new
                    {
                        SalesRecordId = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalesRecordId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Courses", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Courses", "CourseName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Lectures", "LectureName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesRecords", "UserId", "dbo.Users");
            DropForeignKey("dbo.Invoices", "UserId", "dbo.Users");
            DropForeignKey("dbo.Invoices", "CourseId", "dbo.Courses");
            DropIndex("dbo.SalesRecords", new[] { "UserId" });
            DropIndex("dbo.Invoices", new[] { "CourseId" });
            DropIndex("dbo.Invoices", new[] { "UserId" });
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Lectures", "LectureName", c => c.String());
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "CourseName", c => c.String(nullable: false));
            DropColumn("dbo.Courses", "Price");
            DropTable("dbo.SalesRecords");
            DropTable("dbo.Invoices");
        }
    }
}
