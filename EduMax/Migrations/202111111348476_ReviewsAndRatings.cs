namespace EduMax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewsAndRatings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReviewsAndRatings",
                c => new
                    {
                        ReviewAndRatingId = c.Int(nullable: false, identity: true),
                        Rating = c.Single(nullable: false),
                        Review = c.String(),
                        CourseId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewAndRatingId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewsAndRatings", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReviewsAndRatings", "CourseId", "dbo.Courses");
            DropIndex("dbo.ReviewsAndRatings", new[] { "UserId" });
            DropIndex("dbo.ReviewsAndRatings", new[] { "CourseId" });
            DropTable("dbo.ReviewsAndRatings");
        }
    }
}
