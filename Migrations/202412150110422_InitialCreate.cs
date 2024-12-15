namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandID = c.String(nullable: false, maxLength: 128),
                        BrandName = c.String(),
                        BrandDescription = c.String(),
                        BrandPic = c.String(),
                        BrandBackground = c.String(),
                    })
                .PrimaryKey(t => t.BrandID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.String(nullable: false, maxLength: 128),
                        ProductDescription = c.String(),
                        BrandID = c.String(nullable: false, maxLength: 128),
                        CategoryID = c.String(nullable: false, maxLength: 128),
                        AvailabilityStatus = c.String(),
                        Quantity = c.Int(nullable: false),
                        BaoHanh = c.String(),
                        ProductName = c.String(nullable: false),
                        ProductPic = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        SalePrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.BrandID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemID = c.Int(nullable: false, identity: true),
                        shopping_quantity = c.Int(nullable: false),
                        ProductID = c.String(maxLength: 128),
                        AvailableQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartItemID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.String(nullable: false, maxLength: 128),
                        CategoryName = c.String(),
                        CategoryDescription = c.String(),
                        CategoryPic = c.String(),
                        Published = c.Int(),
                        CategoryBackground = c.String(),
                        CategoryAvatar = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.KhuyenMaiSanPhams",
                c => new
                    {
                        maKhuyenMai = c.String(nullable: false, maxLength: 50),
                        maSanPham = c.String(nullable: false, maxLength: 128),
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
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ProductID = c.String(nullable: false, maxLength: 128),
                        OrderId = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ProductID, t.OrderId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        CustomerName = c.String(nullable: false),
                        CustomerPhone = c.String(nullable: false),
                        CustomerAddress = c.String(nullable: false),
<<<<<<<< HEAD:Migrations/202412150120299_InitialCreate.cs
========
                        CustomerProvince = c.String(nullable: false),
>>>>>>>> a1c937059287ae52724edf6312e882847733c9dc:Migrations/202412150110422_InitialCreate.cs
                        CustomerDistrict = c.String(nullable: false),
                        CustomerWard = c.String(nullable: false),
                        CustomerEmail = c.String(nullable: false),
                        PaymentMethod = c.String(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Birthday = c.DateTime(),
                        Address = c.String(),
                        City = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ChiTietPhieuDats",
                c => new
                    {
                        MaPhieuDat = c.String(nullable: false, maxLength: 128),
                        ProductID = c.String(nullable: false, maxLength: 128),
                        SoLuongDat = c.Int(nullable: false),
                        SoLuongNhan = c.Int(nullable: false),
                        DonGia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TongTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.MaPhieuDat, t.ProductID })
                .ForeignKey("dbo.PhieuDats", t => t.MaPhieuDat)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.MaPhieuDat)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.ChiTietPhieuNhaps",
                c => new
                    {
                        MaPhieuDat = c.String(nullable: false, maxLength: 128),
                        ProductID = c.String(nullable: false, maxLength: 128),
                        MaPhieuNhap = c.String(nullable: false, maxLength: 128),
                        SoLuong = c.Int(nullable: false),
                        DonGia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TongTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.MaPhieuDat, t.ProductID, t.MaPhieuNhap })
                .ForeignKey("dbo.ChiTietPhieuDats", t => new { t.MaPhieuDat, t.ProductID }, cascadeDelete: true)
                .ForeignKey("dbo.PhieuNhaps", t => t.MaPhieuNhap, cascadeDelete: true)
                .Index(t => new { t.MaPhieuDat, t.ProductID })
                .Index(t => t.MaPhieuNhap);
            
            CreateTable(
                "dbo.PhieuNhaps",
                c => new
                    {
                        MaPhieuNhap = c.String(nullable: false, maxLength: 128),
                        MaPhieuDat = c.String(maxLength: 128),
                        NgayNhap = c.DateTime(nullable: false),
                        SoLan = c.Int(nullable: false),
                        TongTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MaPhieuNhap)
                .ForeignKey("dbo.PhieuDats", t => t.MaPhieuDat)
                .Index(t => t.MaPhieuDat);
            
            CreateTable(
                "dbo.PhieuDats",
                c => new
                    {
                        MaPhieuDat = c.String(nullable: false, maxLength: 128),
                        MaNhaCungCap = c.String(nullable: false, maxLength: 10),
                        NgayLap = c.DateTime(nullable: false),
                        NgayCapNhat = c.DateTime(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        TongTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrangThai = c.String(),
                    })
                .PrimaryKey(t => t.MaPhieuDat)
                .ForeignKey("dbo.NhaCungCaps", t => t.MaNhaCungCap)
                .Index(t => t.MaNhaCungCap);
            
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
<<<<<<<< HEAD:Migrations/202412150120299_InitialCreate.cs
            DropForeignKey("dbo.ChiTietPhieuDats", "MaPhieuDat", "dbo.PhieuDats");
            DropForeignKey("dbo.ChiTietPhieuDats", "ProductID", "dbo.Products");
========
            DropForeignKey("dbo.ChiTietPhieuDats", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ChiTietPhieuDats", "MaPhieuDat", "dbo.PhieuDats");
>>>>>>>> a1c937059287ae52724edf6312e882847733c9dc:Migrations/202412150110422_InitialCreate.cs
            DropForeignKey("dbo.ChiTietPhieuNhaps", "MaPhieuNhap", "dbo.PhieuNhaps");
            DropForeignKey("dbo.PhieuNhaps", "MaPhieuDat", "dbo.PhieuDats");
            DropForeignKey("dbo.PhieuDats", "MaNhaCungCap", "dbo.NhaCungCaps");
            DropForeignKey("dbo.ChiTietPhieuNhaps", new[] { "MaPhieuDat", "ProductID" }, "dbo.ChiTietPhieuDats");
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.KhuyenMaiSanPhams", "maSanPham", "dbo.Products");
            DropForeignKey("dbo.KhuyenMaiSanPhams", "maKhuyenMai", "dbo.KhuyenMais");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CartItems", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "BrandID", "dbo.Brands");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PhieuDats", new[] { "MaNhaCungCap" });
            DropIndex("dbo.PhieuNhaps", new[] { "MaPhieuDat" });
            DropIndex("dbo.ChiTietPhieuNhaps", new[] { "MaPhieuNhap" });
            DropIndex("dbo.ChiTietPhieuNhaps", new[] { "MaPhieuDat", "ProductID" });
            DropIndex("dbo.ChiTietPhieuDats", new[] { "ProductID" });
            DropIndex("dbo.ChiTietPhieuDats", new[] { "MaPhieuDat" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.KhuyenMaiSanPhams", new[] { "maSanPham" });
            DropIndex("dbo.KhuyenMaiSanPhams", new[] { "maKhuyenMai" });
            DropIndex("dbo.CartItems", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "BrandID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.NhaCungCaps");
            DropTable("dbo.PhieuDats");
            DropTable("dbo.PhieuNhaps");
            DropTable("dbo.ChiTietPhieuNhaps");
            DropTable("dbo.ChiTietPhieuDats");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.KhuyenMais");
            DropTable("dbo.KhuyenMaiSanPhams");
            DropTable("dbo.Categories");
            DropTable("dbo.CartItems");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
        }
    }
}
