namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZalenBijwerken : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Zalen", "Gereserveerd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Zalen", "Gereserveerd", c => c.Boolean(nullable: false));
        }
    }
}
