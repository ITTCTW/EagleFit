namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeactivatieProppertiesToevoegen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Abonnementen", "Actief", c => c.Boolean(nullable: false));
            AddColumn("dbo.Adressen", "Actief", c => c.Boolean(nullable: false));
            AddColumn("dbo.Leden", "Actief", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clubs", "Actief", c => c.Boolean(nullable: false));
            AddColumn("dbo.Groepslessen", "Actief", c => c.Boolean(nullable: false));
            AddColumn("dbo.Zalen", "Actief", c => c.Boolean(nullable: false));
            AddColumn("dbo.Toestellen", "Actief", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Toestellen", "Actief");
            DropColumn("dbo.Zalen", "Actief");
            DropColumn("dbo.Groepslessen", "Actief");
            DropColumn("dbo.Clubs", "Actief");
            DropColumn("dbo.Leden", "Actief");
            DropColumn("dbo.Adressen", "Actief");
            DropColumn("dbo.Abonnementen", "Actief");
        }
    }
}
