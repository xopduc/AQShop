namespace AQShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeModelProduct_old : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "MoreImages", c => c.String(storeType: "xml"));
            DropColumn("dbo.Products", "MoreImages_Prefix");
            DropColumn("dbo.Products", "MoreImages_IsEmpty");
            DropColumn("dbo.Products", "MoreImages_InnerXml");
            DropColumn("dbo.Products", "MoreImages_InnerText");
            DropColumn("dbo.Products", "MoreImages_Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "MoreImages_Value", c => c.String());
            AddColumn("dbo.Products", "MoreImages_InnerText", c => c.String());
            AddColumn("dbo.Products", "MoreImages_InnerXml", c => c.String());
            AddColumn("dbo.Products", "MoreImages_IsEmpty", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "MoreImages_Prefix", c => c.String());
            DropColumn("dbo.Products", "MoreImages");
        }
    }
}
