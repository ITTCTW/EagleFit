namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AanpassingReserveringen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reserveringen", "Reserveringsdatum", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reserveringen", "Actief", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Reserveringen", "ZaalId");
            CreateIndex("dbo.Reserveringen", "MedewerkersId");
            AddForeignKey("dbo.Reserveringen", "MedewerkersId", "dbo.Medewerkers", "MedewerkersId", cascadeDelete: true);
            AddForeignKey("dbo.Reserveringen", "ZaalId", "dbo.Zalen", "ZaalId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reserveringen", "ZaalId", "dbo.Zalen");
            DropForeignKey("dbo.Reserveringen", "MedewerkersId", "dbo.Medewerkers");
            DropIndex("dbo.Reserveringen", new[] { "MedewerkersId" });
            DropIndex("dbo.Reserveringen", new[] { "ZaalId" });
            DropColumn("dbo.Reserveringen", "Actief");
            DropColumn("dbo.Reserveringen", "Reserveringsdatum");
        }
    }
}
