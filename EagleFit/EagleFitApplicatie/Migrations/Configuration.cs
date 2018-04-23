namespace V1.Migrations
{
    using EL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Context.EagleFitContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.Context.EagleFitContext ctx)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            ctx.Medewerkers.AddOrUpdate(new Medewerker
            {
                MedewerkersId = 1,
                Actief = true,
                MedewerkersStatus = "Admin",
                Salaris = 1800,
                PersoonsId = 1
            });
            ctx.Abonnementen.AddOrUpdate(new Abonnement
            {
                AbonnementId = 1,
                Naam = "Eagle Basic",
                Omschrijving = "Basisabonnement met enkel toegang tot de toestellen",
                PrijsPerMaand = 9.99M
            });
            ctx.Adressen.AddOrUpdate(new Adres
            {
                AdresId = 1,
                ClubId = 1,
                Beschrijving = "Eagle Fit Eke",
                Straat = "Eedstraat",
                Huisnummer = 17,
                Postcode = 9810,
                Gemeente = "Eke",
                Latitude = 50.971332,
                Longitude = 3.662011
            },
            new Adres
            {
                AdresId = 2,
                ClubId = 2,
                Beschrijving = "Eagle Fit Deinze",
                Straat = "Schave",
                Huisnummer = 13,
                Postcode = 9800,
                Gemeente = "Deinze",
                Latitude = 50.989913,
                Longitude = 3.520195
            });
            ctx.Clubs.AddOrUpdate(new Club
            {
                ClubId = 1,
                AdresId = 1,
                Naam = "Eagle Fit Eke",
                TelefoonNummer = "092782563",
                Kalender = "~/Images/GroepslesPlanningen/EagleFitEke.PNG",                
            },
            new Club
            {
                ClubId = 2,
                AdresId = 2,
                Naam = "Eagle Fit Deinze",
                TelefoonNummer = "092881548",
                Kalender = "~/Images/GroepslesPlanningen/EagleFitEke.PNG",
            });
            ctx.Groepslessen.AddOrUpdate(
            new Groepsles
            {
                GroepslesId = 1,
                Naam = "Total Abs",
                Beschrijving = "30min. trainen op de buikspieren"
            },
            new Groepsles
            {
                GroepslesId = 2,
                Naam = "Les Mills Grit Cardio",
                Beschrijving = "intensieve HIIT(high intensive interval training) cardio van 30min."
            },
            new Groepsles
            {
                GroepslesId = 3,
                Naam = "Bodystep",
                Beschrijving = "1u training met de step"
            },
            new Groepsles
            {
                GroepslesId = 4,
                Naam = "Bodypump",
                Beschrijving = "1u training met gewichten en een barbell(bar met gewichten aan)"
            },
            new Groepsles
            {
                GroepslesId = 5,
                Naam = "Les Mills Grit Strength",
                Beschrijving = "30min. HIIT training maar meer gefocust op kracht dan cardio"
            },
            new Groepsles
            {
                GroepslesId = 6,
                Naam = "bodyattack",
                Beschrijving = "1u cardio training, gebaseerd op aërobics"
            },
            new Groepsles
            {
                GroepslesId = 7,
                Naam = "Bodycombat",
                Beschrijving = "1u cardio training, gebaseerd op verschillende gevechtsporten"
            },
            new Groepsles
            {
                GroepslesId = 8,
                Naam = "Bodybalance",
                Beschrijving = "1u training die tai chi, yoga en pilates combineert"
            },
            new Groepsles
            {
                GroepslesId = 9,
                Naam = "Les Mills Grit Plyo",
                Beschrijving = "30min. intensieve HIIT training(high intensive interval training) gefocust op plyo"
            });
            ctx.Zalen.AddOrUpdate(
                new Zaal
                {
                    ZaalId = 1,
                    Zaalnaam = "Zaal 1",
                    ClubId = 1,
                    GrooteInVierkanteMeter = 30
                });
        }
    }
}
