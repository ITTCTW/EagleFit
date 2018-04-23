namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitEagleFitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abonnementen",
                c => new
                    {
                        AbonnementId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false, maxLength: 100),
                        Omschrijving = c.String(nullable: false),
                        PrijsPerMaand = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AbonnementId);
            
            CreateTable(
                "dbo.Adressen",
                c => new
                    {
                        AdresId = c.Int(nullable: false, identity: true),
                        Straat = c.String(nullable: false, maxLength: 100),
                        Huisnummer = c.Int(nullable: false),
                        Postcode = c.Int(nullable: false),
                        Gemeente = c.String(nullable: false, maxLength: 100),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        ClubId = c.Int(nullable: false),
                        Beschrijving = c.String(),
                    })
                .PrimaryKey(t => t.AdresId);
            
            CreateTable(
                "dbo.Leden",
                c => new
                    {
                        LidNummer = c.Int(nullable: false, identity: true),
                        PersoonId = c.Int(nullable: false),
                        AbonnementId = c.Int(nullable: false),
                        ClubId = c.Int(nullable: false),
                        Persoon_PersoonsId = c.Int(),
                        Adres_AdresId = c.Int(),
                    })
                .PrimaryKey(t => t.LidNummer)
                .ForeignKey("dbo.Personen", t => t.Persoon_PersoonsId)
                .ForeignKey("dbo.Adressen", t => t.Adres_AdresId)
                .Index(t => t.Persoon_PersoonsId)
                .Index(t => t.Adres_AdresId);
            
            CreateTable(
                "dbo.Groepslessen",
                c => new
                    {
                        GroepslesId = c.Int(nullable: false, identity: true),
                        naam = c.String(nullable: false, maxLength: 100),
                        Beschrijving = c.String(),
                    })
                .PrimaryKey(t => t.GroepslesId);
            
            CreateTable(
                "dbo.Clubs",
                c => new
                    {
                        ClubId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false, maxLength: 200),
                        AdresId = c.Int(nullable: false),
                        TelefoonNummer = c.String(nullable: false, maxLength: 20),
                        Kalender = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ClubId);
            
            CreateTable(
                "dbo.Personen",
                c => new
                    {
                        PersoonsId = c.Int(nullable: false, identity: true),
                        Voornaam = c.String(nullable: false, maxLength: 100),
                        Familienaam = c.String(nullable: false, maxLength: 100),
                        TelefoonNummer = c.String(nullable: false, maxLength: 20),
                        Geslacht = c.String(nullable: false, maxLength: 5),
                        Email = c.String(nullable: false, maxLength: 100),
                        AdresId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersoonsId)
                .ForeignKey("dbo.Adressen", t => t.AdresId, cascadeDelete: true)
                .Index(t => t.AdresId);
            
            CreateTable(
                "dbo.Betalingen",
                c => new
                    {
                        BetalingsId = c.Int(nullable: false, identity: true),
                        Lidnummer = c.Int(nullable: false),
                        Datum = c.DateTime(nullable: false),
                        Bedrag = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Omschrijving = c.String(nullable: false),
                        Betaald = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BetalingsId);
            
            CreateTable(
                "dbo.Medewerkers",
                c => new
                    {
                        MedewerkersId = c.Int(nullable: false, identity: true),
                        PersoonsId = c.Int(nullable: false),
                        AdresId = c.Int(nullable: false),
                        Salaris = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VakantieId = c.Int(nullable: false),
                        MedewerkersStatus = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MedewerkersId);
            
            CreateTable(
                "dbo.Reserveringen",
                c => new
                    {
                        ReserveringsId = c.Int(nullable: false, identity: true),
                        ZaalId = c.Int(nullable: false),
                        MedewerkersId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReserveringsId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false, maxLength: 100),
                        Klacht = c.String(nullable: false),
                        Actief = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TicketId);
            
            CreateTable(
                "dbo.Toestellen",
                c => new
                    {
                        ToestelId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false, maxLength: 100),
                        AankoopDatum = c.DateTime(nullable: false),
                        ProbleemId = c.Int(nullable: false),
                        Handleiding = c.String(),
                        Kuisproducten = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ToestelId);
            
            CreateTable(
                "dbo.ToestelProblemen",
                c => new
                    {
                        ProbleemId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false, maxLength: 100),
                        Omschrijving = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProbleemId);
            
            CreateTable(
                "dbo.Vakanties",
                c => new
                    {
                        VakantieId = c.Int(nullable: false, identity: true),
                        BeginDatum = c.DateTime(nullable: false),
                        EindDatum = c.DateTime(nullable: false),
                        Goedgekeurd = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VakantieId);
            
            CreateTable(
                "dbo.Zalen",
                c => new
                    {
                        ZaalId = c.Int(nullable: false, identity: true),
                        Zaalnaam = c.String(nullable: false, maxLength: 100),
                        GrooteInVierkanteMeter = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gereserveerd = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ZaalId);
            
            CreateTable(
                "dbo.Club_Groepsles",
                c => new
                    {
                        ClubId = c.Int(nullable: false),
                        GroepslesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClubId, t.GroepslesId })
                .ForeignKey("dbo.Clubs", t => t.ClubId, cascadeDelete: true)
                .ForeignKey("dbo.Groepslessen", t => t.GroepslesId, cascadeDelete: true)
                .Index(t => t.ClubId)
                .Index(t => t.GroepslesId);
            
            CreateTable(
                "dbo.Groepsles_Lid",
                c => new
                    {
                        GroepslesId = c.Int(nullable: false),
                        LidNummer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroepslesId, t.LidNummer })
                .ForeignKey("dbo.Groepslessen", t => t.GroepslesId, cascadeDelete: true)
                .ForeignKey("dbo.Leden", t => t.LidNummer, cascadeDelete: true)
                .Index(t => t.GroepslesId)
                .Index(t => t.LidNummer);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leden", "Adres_AdresId", "dbo.Adressen");
            DropForeignKey("dbo.Leden", "Persoon_PersoonsId", "dbo.Personen");
            DropForeignKey("dbo.Personen", "AdresId", "dbo.Adressen");
            DropForeignKey("dbo.Groepsles_Lid", "LidNummer", "dbo.Leden");
            DropForeignKey("dbo.Groepsles_Lid", "GroepslesId", "dbo.Groepslessen");
            DropForeignKey("dbo.Club_Groepsles", "GroepslesId", "dbo.Groepslessen");
            DropForeignKey("dbo.Club_Groepsles", "ClubId", "dbo.Clubs");
            DropIndex("dbo.Groepsles_Lid", new[] { "LidNummer" });
            DropIndex("dbo.Groepsles_Lid", new[] { "GroepslesId" });
            DropIndex("dbo.Club_Groepsles", new[] { "GroepslesId" });
            DropIndex("dbo.Club_Groepsles", new[] { "ClubId" });
            DropIndex("dbo.Personen", new[] { "AdresId" });
            DropIndex("dbo.Leden", new[] { "Adres_AdresId" });
            DropIndex("dbo.Leden", new[] { "Persoon_PersoonsId" });
            DropTable("dbo.Groepsles_Lid");
            DropTable("dbo.Club_Groepsles");
            DropTable("dbo.Zalen");
            DropTable("dbo.Vakanties");
            DropTable("dbo.ToestelProblemen");
            DropTable("dbo.Toestellen");
            DropTable("dbo.Tickets");
            DropTable("dbo.Reserveringen");
            DropTable("dbo.Medewerkers");
            DropTable("dbo.Betalingen");
            DropTable("dbo.Personen");
            DropTable("dbo.Clubs");
            DropTable("dbo.Groepslessen");
            DropTable("dbo.Leden");
            DropTable("dbo.Adressen");
            DropTable("dbo.Abonnementen");
        }
    }
}
