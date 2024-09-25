namespace DoAnWebGamingGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "AvailabilityStatus", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "AvailabilityStatus", c => c.String(nullable: false));
        }
    }
}
