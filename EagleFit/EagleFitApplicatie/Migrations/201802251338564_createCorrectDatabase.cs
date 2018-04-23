namespace V1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createCorrectDatabase : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Leden", "ClubId");
            AddForeignKey("dbo.Leden", "ClubId", "dbo.Clubs", "ClubId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leden", "ClubId", "dbo.Clubs");
            DropIndex("dbo.Leden", new[] { "ClubId" });
        }
    }
}
