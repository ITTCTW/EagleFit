namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KuisproductProppertieChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Toestellen", "Kuisproduct", c => c.Int(nullable: false));
            DropColumn("dbo.Toestellen", "Kuisproducten");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Toestellen", "Kuisproducten", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Toestellen", "Kuisproduct");
        }
    }
}
