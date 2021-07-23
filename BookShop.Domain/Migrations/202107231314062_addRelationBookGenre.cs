namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRelationBookGenre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Genre_GenreId", c => c.Int());
            CreateIndex("dbo.Books", "Genre_GenreId");
            AddForeignKey("dbo.Books", "Genre_GenreId", "dbo.Genres", "GenreId");
            DropColumn("dbo.Books", "Genre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Genre", c => c.String(nullable: false));
            DropForeignKey("dbo.Books", "Genre_GenreId", "dbo.Genres");
            DropIndex("dbo.Books", new[] { "Genre_GenreId" });
            DropColumn("dbo.Books", "Genre_GenreId");
        }
    }
}
