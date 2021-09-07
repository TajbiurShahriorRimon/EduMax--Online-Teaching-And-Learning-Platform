namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_Credential_Annotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Credentials", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Institution", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Institution", c => c.String());
            AlterColumn("dbo.Credentials", "Password", c => c.String());
        }
    }
}
