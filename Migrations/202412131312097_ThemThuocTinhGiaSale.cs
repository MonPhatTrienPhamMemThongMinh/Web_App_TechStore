namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemThuocTinhGiaSale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SalePrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "SalePrice");
        }
    }
}
