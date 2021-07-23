namespace BookShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAuthorInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "AuthorDesc", c => c.String());
            DropColumn("dbo.Authors", "AuthorLastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Authors", "AuthorLastName", c => c.String());
            DropColumn("dbo.Authors", "AuthorDesc");
        }
    }
}
