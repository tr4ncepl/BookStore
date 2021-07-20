namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletebrokenPublisher : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Publishers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        PublisherName = c.String(),
                        PublisherDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PublisherId);
            
        }
    }
}
