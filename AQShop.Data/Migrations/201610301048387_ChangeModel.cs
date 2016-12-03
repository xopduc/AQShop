namespace AQShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", new[] { "OrderDetail_OrderID", "OrderDetail_ProductID" }, "dbo.OrderDetails");
            DropIndex("dbo.Orders", new[] { "OrderDetail_OrderID", "OrderDetail_ProductID" });
            DropPrimaryKey("dbo.Tags");
            AlterColumn("dbo.Tags", "ID", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.Tags", "ID");
            CreateIndex("dbo.OrderDetails", "OrderID");
            CreateIndex("dbo.OrderDetails", "ProductID");
            CreateIndex("dbo.PostTag", "PostID");
            CreateIndex("dbo.PostTag", "TagID");
            CreateIndex("dbo.ProductTags", "ProductID");
            CreateIndex("dbo.ProductTags", "TagID");
            AddForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders", "ID", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PostTag", "PostID", "dbo.Posts", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PostTag", "TagID", "dbo.Tags", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductTags", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductTags", "TagID", "dbo.Tags", "ID", cascadeDelete: true);
            DropColumn("dbo.Orders", "OrderDetail_OrderID");
            DropColumn("dbo.Orders", "OrderDetail_ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderDetail_ProductID", c => c.Int());
            AddColumn("dbo.Orders", "OrderDetail_OrderID", c => c.Int());
            DropForeignKey("dbo.ProductTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.ProductTags", "ProductID", "dbo.Products");
            DropForeignKey("dbo.PostTag", "TagID", "dbo.Tags");
            DropForeignKey("dbo.PostTag", "PostID", "dbo.Posts");
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropIndex("dbo.ProductTags", new[] { "TagID" });
            DropIndex("dbo.ProductTags", new[] { "ProductID" });
            DropIndex("dbo.PostTag", new[] { "TagID" });
            DropIndex("dbo.PostTag", new[] { "PostID" });
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropPrimaryKey("dbo.Tags");
            AlterColumn("dbo.Tags", "ID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Tags", "ID");
            CreateIndex("dbo.Orders", new[] { "OrderDetail_OrderID", "OrderDetail_ProductID" });
            AddForeignKey("dbo.Orders", new[] { "OrderDetail_OrderID", "OrderDetail_ProductID" }, "dbo.OrderDetails", new[] { "OrderID", "ProductID" });
        }
    }
}
