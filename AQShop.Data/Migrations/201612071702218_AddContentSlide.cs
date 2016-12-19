namespace AQShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContentSlide : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Slides", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Slides", "Content");
        }
    }
}
