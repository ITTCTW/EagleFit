namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeLidModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leden", "TeWijzigenAbonnementId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leden", "TeWijzigenAbonnementId");
        }
    }
}
