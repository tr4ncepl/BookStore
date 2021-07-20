namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletePublishersfromBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers");
            DropIndex("dbo.Books", new[] { "Publisher_PublisherId" });
            DropColumn("dbo.Books", "Publisher_PublisherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Publisher_PublisherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "Publisher_PublisherId");
            AddForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers", "PublisherId", cascadeDelete: true);
        }
    }
}
