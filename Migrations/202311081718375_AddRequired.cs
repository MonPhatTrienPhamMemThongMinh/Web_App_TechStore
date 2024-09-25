namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "BrandID", "dbo.Brands");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "BrandID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            AlterColumn("dbo.Products", "BrandID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Products", "CategoryID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Products", "ProductName", c => c.String(nullable: false));
            CreateIndex("dbo.Products", "BrandID");
            CreateIndex("dbo.Products", "CategoryID");
            AddForeignKey("dbo.Products", "BrandID", "dbo.Brands", "BrandID", cascadeDelete: true);
            AddForeignKey("dbo.Products", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandID", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "BrandID" });
            AlterColumn("dbo.Products", "ProductName", c => c.String());
            AlterColumn("dbo.Products", "CategoryID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "BrandID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "CategoryID");
            CreateIndex("dbo.Products", "BrandID");
            AddForeignKey("dbo.Products", "CategoryID", "dbo.Categories", "CategoryID");
            AddForeignKey("dbo.Products", "BrandID", "dbo.Brands", "BrandID");
        }
    }
}
