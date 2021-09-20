namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesRecord_Invoice_Updated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.SalesRecords", "UserId", "dbo.Users");
            DropIndex("dbo.Invoices", new[] { "CourseId" });
            DropIndex("dbo.SalesRecords", new[] { "UserId" });
            AddColumn("dbo.Invoices", "TotalAmount", c => c.Double(nullable: false));
            AddColumn("dbo.SalesRecords", "CourseId", c => c.Int(nullable: false));
            AddColumn("dbo.SalesRecords", "InvoiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Credentials", "Password", c => c.String(nullable: false, maxLength: 15));
            CreateIndex("dbo.SalesRecords", "CourseId");
            CreateIndex("dbo.SalesRecords", "InvoiceId");
            AddForeignKey("dbo.SalesRecords", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
            AddForeignKey("dbo.SalesRecords", "InvoiceId", "dbo.Invoices", "InvoiceId", cascadeDelete: true);
            DropColumn("dbo.Invoices", "Amount");
            DropColumn("dbo.Invoices", "CourseId");
            DropColumn("dbo.SalesRecords", "Date");
            DropColumn("dbo.SalesRecords", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesRecords", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.SalesRecords", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Invoices", "CourseId", c => c.Int(nullable: false));
            AddColumn("dbo.Invoices", "Amount", c => c.Double(nullable: false));
            DropForeignKey("dbo.SalesRecords", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.SalesRecords", "CourseId", "dbo.Courses");
            DropIndex("dbo.SalesRecords", new[] { "InvoiceId" });
            DropIndex("dbo.SalesRecords", new[] { "CourseId" });
            AlterColumn("dbo.Credentials", "Password", c => c.String(nullable: false));
            DropColumn("dbo.SalesRecords", "InvoiceId");
            DropColumn("dbo.SalesRecords", "CourseId");
            DropColumn("dbo.Invoices", "TotalAmount");
            CreateIndex("dbo.SalesRecords", "UserId");
            CreateIndex("dbo.Invoices", "CourseId");
            AddForeignKey("dbo.SalesRecords", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Invoices", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
        }
    }
}
