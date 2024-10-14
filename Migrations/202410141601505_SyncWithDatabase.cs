namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncWithDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NhaCungCaps",
                c => new
                    {
                        maNhaCungCap = c.String(nullable: false, maxLength: 10),
                        tenNhaCungCap = c.String(nullable: false, maxLength: 100),
                        soDienThoai = c.String(maxLength: 15),
                        diaChi = c.String(maxLength: 255),
                        email = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.maNhaCungCap);
            
            CreateTable(
                "dbo.ChiTietPhieuNhaps",
                c => new
                    {
                        maPhieuNhap = c.String(nullable: false, maxLength: 128),
                        ProductID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.maPhieuNhap, t.ProductID })
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.PhieuNhaps", t => t.maPhieuNhap, cascadeDelete: true)
                .Index(t => t.maPhieuNhap)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.PhieuNhaps",
                c => new
                    {
                        maPhieuNhap = c.String(nullable: false, maxLength: 128),
                        maNhanVien = c.String(nullable: false),
                        maNhaCungCap = c.String(nullable: false, maxLength: 10),
                        ngayLap = c.DateTime(nullable: false),
                        tongTien = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.maPhieuNhap)
                .ForeignKey("dbo.NhaCungCaps", t => t.maNhaCungCap, cascadeDelete: true)
                .Index(t => t.maNhaCungCap);
            
            AddColumn("dbo.Products", "maNhaCungCap", c => c.String(maxLength: 10));
            CreateIndex("dbo.Products", "maNhaCungCap");
            AddForeignKey("dbo.Products", "maNhaCungCap", "dbo.NhaCungCaps", "maNhaCungCap");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChiTietPhieuNhaps", "maPhieuNhap", "dbo.PhieuNhaps");
            DropForeignKey("dbo.PhieuNhaps", "maNhaCungCap", "dbo.NhaCungCaps");
            DropForeignKey("dbo.ChiTietPhieuNhaps", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "maNhaCungCap", "dbo.NhaCungCaps");
            DropIndex("dbo.PhieuNhaps", new[] { "maNhaCungCap" });
            DropIndex("dbo.ChiTietPhieuNhaps", new[] { "ProductID" });
            DropIndex("dbo.ChiTietPhieuNhaps", new[] { "maPhieuNhap" });
            DropIndex("dbo.Products", new[] { "maNhaCungCap" });
            DropColumn("dbo.Products", "maNhaCungCap");
            DropTable("dbo.PhieuNhaps");
            DropTable("dbo.ChiTietPhieuNhaps");
            DropTable("dbo.NhaCungCaps");
        }
    }
}
