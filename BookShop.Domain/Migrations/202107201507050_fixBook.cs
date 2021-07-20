namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers");
            DropIndex("dbo.Books", new[] { "Publisher_PublisherId" });
            AlterColumn("dbo.Books", "Publisher_PublisherId", c => c.Int());
            CreateIndex("dbo.Books", "Publisher_PublisherId");
            AddForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers", "PublisherId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers");
            DropIndex("dbo.Books", new[] { "Publisher_PublisherId" });
            AlterColumn("dbo.Books", "Publisher_PublisherId", c => c.Int());
            CreateIndex("dbo.Books", "Publisher_PublisherId");
            AddForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers", "PublisherId");
        }
    }
}
