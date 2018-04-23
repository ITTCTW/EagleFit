namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medewerkers", "Actief", c => c.Boolean(nullable: false));
            AddColumn("dbo.ToestelProblemen", "ToestelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Medewerkers", "PersoonsId");
            AddForeignKey("dbo.Medewerkers", "PersoonsId", "dbo.Personen", "PersoonsId", cascadeDelete: true);
            DropColumn("dbo.Medewerkers", "AdresId");
            DropColumn("dbo.Toestellen", "ProbleemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Toestellen", "ProbleemId", c => c.Int(nullable: false));
            AddColumn("dbo.Medewerkers", "AdresId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Medewerkers", "PersoonsId", "dbo.Personen");
            DropIndex("dbo.Medewerkers", new[] { "PersoonsId" });
            DropColumn("dbo.ToestelProblemen", "ToestelId");
            DropColumn("dbo.Medewerkers", "Actief");
        }
    }
}
