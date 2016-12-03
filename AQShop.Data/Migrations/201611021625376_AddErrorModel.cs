namespace AQShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddErrorModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.OrderDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Products", "CreateBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.Products", "UpdateBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostCategories", "CreateDate", c => c.DateTime());
            AddColumn("dbo.PostCategories", "CreateBy", c => c.String(maxLength: 256));
            AddColumn("dbo.PostCategories", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.PostCategories", "UpdateBy", c => c.String(maxLength: 256));
            AddColumn("dbo.PostCategories", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.PostCategories", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.PostCategories", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Posts", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Posts", "CreateBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Posts", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.Posts", "UpdateBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Posts", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Posts", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Posts", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Status");
            DropColumn("dbo.Posts", "MetaDescription");
            DropColumn("dbo.Posts", "MetaKeyword");
            DropColumn("dbo.Posts", "UpdateBy");
            DropColumn("dbo.Posts", "UpdateTime");
            DropColumn("dbo.Posts", "CreateBy");
            DropColumn("dbo.Posts", "CreateDate");
            DropColumn("dbo.PostCategories", "Status");
            DropColumn("dbo.PostCategories", "MetaDescription");
            DropColumn("dbo.PostCategories", "MetaKeyword");
            DropColumn("dbo.PostCategories", "UpdateBy");
            DropColumn("dbo.PostCategories", "UpdateTime");
            DropColumn("dbo.PostCategories", "CreateBy");
            DropColumn("dbo.PostCategories", "CreateDate");
            DropColumn("dbo.Products", "Status");
            DropColumn("dbo.Products", "MetaDescription");
            DropColumn("dbo.Products", "MetaKeyword");
            DropColumn("dbo.Products", "UpdateBy");
            DropColumn("dbo.Products", "UpdateTime");
            DropColumn("dbo.Products", "CreateBy");
            DropColumn("dbo.Products", "CreateDate");
            DropColumn("dbo.OrderDetails", "Price");
            DropTable("dbo.Errors");
        }
    }
}
