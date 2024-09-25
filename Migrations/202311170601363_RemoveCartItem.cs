namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCartItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "ProductID", "dbo.Products");
            DropIndex("dbo.CartItems", new[] { "ProductID" });
            DropTable("dbo.CartItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                {
                    CartID = c.String(nullable: false, maxLength: 128),
                    shopping_quantity = c.String(),
                    ProductID = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.CartID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID);
        }
    }
}
