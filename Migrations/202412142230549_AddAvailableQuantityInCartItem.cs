namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvailableQuantityInCartItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "AvailableQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartItems", "AvailableQuantity");
        }
    }
}
