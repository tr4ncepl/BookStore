namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookPublisherRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Publisher_PublisherId", c => c.Int());
            CreateIndex("dbo.Books", "Publisher_PublisherId");
            AddForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers", "PublisherId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Publisher_PublisherId", "dbo.Publishers");
            DropIndex("dbo.Books", new[] { "Publisher_PublisherId" });
            DropColumn("dbo.Books", "Publisher_PublisherId");
        }
    }
}
