namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _ref : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Author_AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers");
            DropIndex("dbo.Books", new[] { "Author_AuthorId" });
            DropIndex("dbo.Books", new[] { "Publisher_PublisherId" });
            AlterColumn("dbo.Books", "Author_AuthorId", c => c.Int());
            AlterColumn("dbo.Books", "Publisher_PublisherId", c => c.Int());
            CreateIndex("dbo.Books", "Author_AuthorId");
            CreateIndex("dbo.Books", "Publisher_PublisherId");
            AddForeignKey("dbo.Books", "Author_AuthorId", "dbo.Authors", "AuthorId");
            AddForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers", "PublisherId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.Books", "Author_AuthorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "Publisher_PublisherId" });
            DropIndex("dbo.Books", new[] { "Author_AuthorId" });
            AlterColumn("dbo.Books", "Publisher_PublisherId", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "Author_AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "Publisher_PublisherId");
            CreateIndex("dbo.Books", "Author_AuthorId");
            AddForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers", "PublisherId", cascadeDelete: true);
            AddForeignKey("dbo.Books", "Author_AuthorId", "dbo.Authors", "AuthorId", cascadeDelete: true);
        }
    }
}
