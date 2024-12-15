namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemThuocTinhProvinceChoOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerProvince", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CustomerProvince");
        }
    }
}
