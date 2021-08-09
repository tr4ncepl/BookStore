namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBookPageNumber : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Books", "PagesNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
           // DropColumn("dbo.Books", "PagesNumber");
        }
    }
}
