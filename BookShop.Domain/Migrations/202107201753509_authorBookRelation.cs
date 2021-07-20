namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authorBookRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Author_AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "Author_AuthorId");
            AddForeignKey("dbo.Books", "Author_AuthorId", "dbo.Authors", "AuthorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Author_AuthorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "Author_AuthorId" });
            DropColumn("dbo.Books", "Author_AuthorId");
        }
    }
}
