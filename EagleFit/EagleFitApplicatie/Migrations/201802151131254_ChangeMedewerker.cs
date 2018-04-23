namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMedewerker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vakanties", "MedewerkersId", c => c.Int(nullable: false));
            DropColumn("dbo.Medewerkers", "VakantieId");            
        }
        
        public override void Down()
        {
           
            AddColumn("dbo.Medewerkers", "VakantieId", c => c.Int(nullable: false));
            DropColumn("dbo.Vakanties", "MedewerkersId");
        }
    }
}
