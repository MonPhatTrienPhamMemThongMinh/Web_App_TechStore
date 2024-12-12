namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class XoaUserIDKhoiPhieuDatPhieuNhap : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PhieuDats", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PhieuNhaps", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.PhieuDats", new[] { "UserID" });
            DropIndex("dbo.PhieuNhaps", new[] { "UserID" });
            DropColumn("dbo.PhieuDats", "UserID");
            DropColumn("dbo.PhieuNhaps", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhieuNhaps", "UserID", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.PhieuDats", "UserID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.PhieuNhaps", "UserID");
            CreateIndex("dbo.PhieuDats", "UserID");
            AddForeignKey("dbo.PhieuNhaps", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PhieuDats", "UserID", "dbo.AspNetUsers", "Id");
        }
    }
}
