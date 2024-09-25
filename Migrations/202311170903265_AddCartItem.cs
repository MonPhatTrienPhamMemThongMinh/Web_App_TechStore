namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                {
                    CartItemID = c.String(nullable: false, maxLength: 128),
                    shopping_quantity = c.String(),
                    ProductID = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.CartItemID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "ProductID", "dbo.Products");
            DropIndex("dbo.CartItems", new[] { "ProductID" });
            DropTable("dbo.CartItems");
        }
    }
}
