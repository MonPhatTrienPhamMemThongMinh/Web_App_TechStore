namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProductNameRow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProductID", c => c.String(maxLength: 128));
            CreateIndex("dbo.OrderDetails", "ProductID");
            AddForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products", "ProductID");
            DropColumn("dbo.OrderDetails", "ProductName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "ProductName", c => c.String());
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropColumn("dbo.OrderDetails", "ProductID");
        }
    }
}
