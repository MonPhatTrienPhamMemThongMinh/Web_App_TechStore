namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartItemIDRow : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CartItems");
            AlterColumn("dbo.CartItems", "CartItemID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CartItems", "CartItemID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CartItems");
            AlterColumn("dbo.CartItems", "CartItemID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CartItems", "CartItemID");
        }
    }
}
