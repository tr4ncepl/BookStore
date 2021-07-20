namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testfix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Books", name: "Publisher_PublisherId", newName: "Publish_PublisherId");
            RenameIndex(table: "dbo.Books", name: "IX_Publisher_PublisherId", newName: "IX_Publish_PublisherId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Books", name: "IX_Publish_PublisherId", newName: "IX_Publisher_PublisherId");
            RenameColumn(table: "dbo.Books", name: "Publish_PublisherId", newName: "Publisher_PublisherId");
        }
    }
}
