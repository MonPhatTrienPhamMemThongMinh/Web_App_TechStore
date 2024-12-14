namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemThuocTinhChoBangOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerDistrict", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "CustomerWard", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CustomerWard");
            DropColumn("dbo.Orders", "CustomerDistrict");
        }
    }
}
