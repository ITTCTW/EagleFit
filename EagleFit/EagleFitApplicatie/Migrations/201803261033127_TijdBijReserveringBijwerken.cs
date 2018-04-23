namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TijdBijReserveringBijwerken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reserveringen", "Reserveringsuur", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            
        }
    }
}
