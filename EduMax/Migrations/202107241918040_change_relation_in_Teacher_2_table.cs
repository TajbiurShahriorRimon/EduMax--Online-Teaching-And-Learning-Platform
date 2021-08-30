namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_relation_in_Teacher_2_table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Teachers", new[] { "TeacherId" });
            DropPrimaryKey("dbo.Teachers");
            AlterColumn("dbo.Teachers", "TeacherId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Teachers", "Status", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Teachers", "TeacherId");
            CreateIndex("dbo.Teachers", "TeacherId");
            AddForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Teachers", new[] { "TeacherId" });
            DropPrimaryKey("dbo.Teachers");
            AlterColumn("dbo.Teachers", "Status", c => c.String());
            AlterColumn("dbo.Teachers", "TeacherId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Teachers", "TeacherId");
            CreateIndex("dbo.Teachers", "TeacherId");
            AddForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
        }
    }
}
