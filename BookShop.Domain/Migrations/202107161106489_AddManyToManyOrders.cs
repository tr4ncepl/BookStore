namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManyToManyOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookOrders",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.OrderId })
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.OrderId);
            
           
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        RecName = c.String(),
                        RecAdress = c.String(),
                        RecCity = c.String(),
                        RecState = c.String(),
                        RecZip = c.String(),
                        RecCountry = c.String(),
                        GiftWrap = c.Boolean(nullable: false),
                        TotalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookOrders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.BookOrders", "BookId", "dbo.Books");
            DropIndex("dbo.BookOrders", new[] { "OrderId" });
            DropIndex("dbo.BookOrders", new[] { "BookId" });
            DropTable("dbo.Orders");
            DropTable("dbo.BookOrders");
        }
    }
}
