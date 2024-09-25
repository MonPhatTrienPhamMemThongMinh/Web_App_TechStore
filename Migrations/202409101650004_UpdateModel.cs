namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Id", "dbo.MyProfiles");
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "Id" });
            AddColumn("dbo.Brands", "BrandBackground", c => c.String());
            AddColumn("dbo.Categories", "CategoryAvatar", c => c.String());
            AddColumn("dbo.Orders", "UserId", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "CustomerName", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "CustomerPhone", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "CustomerAddress", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "CustomerEmail", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "PaymentMethod", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "AvailabilityStatus", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "ProductPic", c => c.String(nullable: false));
            AlterColumn("dbo.OrderDetails", "ProductID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.OrderDetails", "ProductID");
            CreateIndex("dbo.OrderDetails", "OrderId");
            AddForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            DropColumn("dbo.Orders", "Id");
            DropTable("dbo.MyProfiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MyProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        DateOfBirth = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "Id", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.String(maxLength: 128));
            AlterColumn("dbo.OrderDetails", "ProductID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Products", "ProductPic", c => c.String());
            AlterColumn("dbo.Products", "Quantity", c => c.String());
            AlterColumn("dbo.Products", "AvailabilityStatus", c => c.String());
            DropColumn("dbo.Orders", "TotalAmount");
            DropColumn("dbo.Orders", "PaymentMethod");
            DropColumn("dbo.Orders", "CustomerEmail");
            DropColumn("dbo.Orders", "CustomerAddress");
            DropColumn("dbo.Orders", "CustomerPhone");
            DropColumn("dbo.Orders", "CustomerName");
            DropColumn("dbo.Orders", "UserId");
            DropColumn("dbo.Categories", "CategoryAvatar");
            DropColumn("dbo.Brands", "BrandBackground");
            CreateIndex("dbo.Orders", "Id");
            CreateIndex("dbo.OrderDetails", "OrderId");
            CreateIndex("dbo.OrderDetails", "ProductID");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "OrderId");
            AddForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products", "ProductID");
            AddForeignKey("dbo.Orders", "Id", "dbo.MyProfiles", "Id", cascadeDelete: true);
        }
    }
}
