namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qqw : DbMigration
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
        }
        
        public override void Down()
        {
            
        }
    }
}
