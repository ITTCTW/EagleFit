namespace DAL.Context
{
    using EL.Models;
    using System.Data.Entity;

    public class EagleFitContext : DbContext
    {
        
        public EagleFitContext()
            : base("name=EagleFitContext")
        {
        }
        public DbSet<Abonnement> Abonnementen { get; set; }
        public DbSet<Adres> Adressen { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Groepsles> Groepslessen { get; set; }
        public DbSet<Lid> Leden { get; set; }
        public DbSet<Persoon> Personen { get; set; }
        public DbSet<Betaling> Betalingen { get; set; }
        public DbSet<Medewerker> Medewerkers { get; set; }
        public DbSet<Vakantie> Vakanties { get; set; }
        public DbSet<Toestel> Toestellen { get; set; }
        public DbSet<ToestelProbleem> ToestelProblemen { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Reservering> Reserveringen { get; set; }
        public DbSet<Zaal> Zalen { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //many to many tabellen aanmaken
            modelBuilder.Entity<Club>().
                HasMany<Groepsles>(c => c.Groepslessen).
                WithMany(g => g.Clubs).
                Map(m =>
                {
                    m.MapLeftKey("ClubId");
                    m.MapRightKey("GroepslesId");
                    m.ToTable("Club_Groepsles");
                });
            modelBuilder.Entity<Groepsles>().
                HasMany<Lid>(g => g.Leden).
                WithMany(l => l.Groepslessen).
                Map(m =>
                {
                    m.MapLeftKey("GroepslesId");
                    m.MapRightKey("LidNummer");
                    m.ToTable("Groepsles_Lid");
                });
        }
    }
        
}