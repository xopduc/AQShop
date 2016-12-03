namespace AQShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStructureEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.ProductCategories", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.PostCategories", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Posts", "UpdateDate", c => c.DateTime());
            DropColumn("dbo.Products", "UpdateTime");
            DropColumn("dbo.ProductCategories", "UpdateTime");
            DropColumn("dbo.PostCategories", "UpdateTime");
            DropColumn("dbo.Posts", "UpdateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.PostCategories", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.ProductCategories", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.Products", "UpdateTime", c => c.DateTime());
            DropColumn("dbo.Posts", "UpdateDate");
            DropColumn("dbo.PostCategories", "UpdateDate");
            DropColumn("dbo.ProductCategories", "UpdateDate");
            DropColumn("dbo.Products", "UpdateDate");
        }
    }
}
