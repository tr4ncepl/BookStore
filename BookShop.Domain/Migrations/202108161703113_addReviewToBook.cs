namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReviewToBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookReviews",
                c => new
                    {
                        BookReviewId = c.Int(nullable: false, identity: true),
                        ReviewDesc = c.String(),
                        ReviewAuthor = c.String(),
                        PublicationDate = c.DateTime(nullable: false),
                        BookRating = c.Int(nullable: false),
                        Book_BookID = c.Int(),
                    })
                .PrimaryKey(t => t.BookReviewId)
                .ForeignKey("dbo.Books", t => t.Book_BookID)
                .Index(t => t.Book_BookID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookReviews", "Book_BookID", "dbo.Books");
            DropIndex("dbo.BookReviews", new[] { "Book_BookID" });
            DropTable("dbo.BookReviews");
        }
    }
}
