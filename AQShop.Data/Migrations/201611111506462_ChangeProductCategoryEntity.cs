namespace AQShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProductCategoryEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "CreateDate", c => c.DateTime());
            AddColumn("dbo.ProductCategories", "CreateBy", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductCategories", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.ProductCategories", "UpdateBy", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductCategories", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductCategories", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductCategories", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "Status");
            DropColumn("dbo.ProductCategories", "MetaDescription");
            DropColumn("dbo.ProductCategories", "MetaKeyword");
            DropColumn("dbo.ProductCategories", "UpdateBy");
            DropColumn("dbo.ProductCategories", "UpdateTime");
            DropColumn("dbo.ProductCategories", "CreateBy");
            DropColumn("dbo.ProductCategories", "CreateDate");
        }
    }
}
