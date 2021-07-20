namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublisherFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Publishers", "PublisherDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publishers", "PublisherDescription", c => c.String());
        }
    }
}
