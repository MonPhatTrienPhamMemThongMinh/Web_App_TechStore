namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemBangKhuyenMai : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KhuyenMaiSanPhams",
                c => new
                    {
                        maKhuyenMai = c.String(nullable: false, maxLength: 50),
                        maSanPham = c.String(nullable: false, maxLength: 128),
                        soLuongToiDa = c.Int(nullable: false),
                        phanTramGiam = c.Decimal(nullable: false, precision: 18, scale: 2),
                        trangThai = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.maKhuyenMai, t.maSanPham })
                .ForeignKey("dbo.KhuyenMais", t => t.maKhuyenMai)
                .ForeignKey("dbo.Products", t => t.maSanPham)
                .Index(t => t.maKhuyenMai)
                .Index(t => t.maSanPham);
            
            CreateTable(
                "dbo.KhuyenMais",
                c => new
                    {
                        maKhuyenMai = c.String(nullable: false, maxLength: 50),
                        tenKhuyenMai = c.String(nullable: false, maxLength: 100),
                        moTa = c.String(maxLength: 255),
                        trangThai = c.String(maxLength: 50),
                        ngayBatDau = c.DateTime(nullable: false),
                        ngayKetThuc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.maKhuyenMai);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KhuyenMaiSanPhams", "maSanPham", "dbo.Products");
            DropForeignKey("dbo.KhuyenMaiSanPhams", "maKhuyenMai", "dbo.KhuyenMais");
            DropIndex("dbo.KhuyenMaiSanPhams", new[] { "maSanPham" });
            DropIndex("dbo.KhuyenMaiSanPhams", new[] { "maKhuyenMai" });
            DropTable("dbo.KhuyenMais");
            DropTable("dbo.KhuyenMaiSanPhams");
        }
    }
}
