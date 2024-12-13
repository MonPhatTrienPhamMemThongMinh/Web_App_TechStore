namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoThuocTinhSoLuongToiDa : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.KhuyenMaiSanPhams", "soLuongToiDa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KhuyenMaiSanPhams", "soLuongToiDa", c => c.Int(nullable: false));
        }
    }
}
