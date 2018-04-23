namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AanpassingZalen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zalen", "ClubId", c => c.Int(nullable: false));
            CreateIndex("dbo.Zalen", "ClubId");
            AddForeignKey("dbo.Zalen", "ClubId", "dbo.Clubs", "ClubId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zalen", "ClubId", "dbo.Clubs");
            DropIndex("dbo.Zalen", new[] { "ClubId" });
            DropColumn("dbo.Zalen", "ClubId");
        }
    }
}
