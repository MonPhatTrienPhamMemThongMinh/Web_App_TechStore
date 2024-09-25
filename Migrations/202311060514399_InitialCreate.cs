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
                    })
                .PrimaryKey(t => t.BrandID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.String(nullable: false, maxLength: 128),
                        ProductDescription = c.String(),
                        BrandID = c.String(maxLength: 128),
                        CategoryID = c.String(maxLength: 128),
                        AvailabilityStatus = c.String(),
                        Quantity = c.String(),
                        BaoHanh = c.String(),
                        ProductName = c.String(),
                        ProductPic = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Brands", t => t.BrandID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .Index(t => t.BrandID)
                .Index(t => t.CategoryID);
            
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
                    })
                .PrimaryKey(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandID", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "BrandID" });
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
        }
    }
}
