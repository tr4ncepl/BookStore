namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAuthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "AuthorName", c => c.String());
            DropColumn("dbo.Authors", "AuhorName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Authors", "AuhorName", c => c.String());
            DropColumn("dbo.Authors", "AuthorName");
        }
    }
}
