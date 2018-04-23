namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TijdBijReservering : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reserveringen", "Reserveringsuur", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reserveringen", "Reserveringsuur");
        }
    }
}
