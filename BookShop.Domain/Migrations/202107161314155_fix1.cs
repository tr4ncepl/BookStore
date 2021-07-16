namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BookOrders", new[] { "BookId" });
            CreateIndex("dbo.BookOrders", "BookID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BookOrders", new[] { "BookID" });
            CreateIndex("dbo.BookOrders", "BookId");
        }
    }
}
