namespace AQShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMigrationChangeNameAlias : DbMigration
    {
        public override void Up()
        {
            RenameColumn("ProductCategories", "ALias", "Alias");
        }
        
        public override void Down()
        {
            RenameColumn("ProductCategories", "Alias", "ALias");
        }
    }
}
