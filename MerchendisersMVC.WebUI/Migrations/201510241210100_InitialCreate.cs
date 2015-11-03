namespace MerchendisersMVC.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Merchendisers",
                c => new
                    {
                        MerchendiserId = c.Int(nullable: false, identity: true),
                        PersonalNo = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address_Street = c.String(),
                        Address_StreetNo = c.Int(nullable: false),
                        Address_PostalCode = c.String(),
                        Address_City = c.String(),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MerchendiserId);
            
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        TownId = c.Int(nullable: false, identity: true),
                        TownName = c.String(),
                    })
                .PrimaryKey(t => t.TownId);
            
            CreateTable(
                "dbo.TownMerchendisers",
                c => new
                    {
                        Town_TownId = c.Int(nullable: false),
                        Merchendiser_MerchendiserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Town_TownId, t.Merchendiser_MerchendiserId })
                .ForeignKey("dbo.Towns", t => t.Town_TownId, cascadeDelete: true)
                .ForeignKey("dbo.Merchendisers", t => t.Merchendiser_MerchendiserId, cascadeDelete: true)
                .Index(t => t.Town_TownId)
                .Index(t => t.Merchendiser_MerchendiserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TownMerchendisers", "Merchendiser_MerchendiserId", "dbo.Merchendisers");
            DropForeignKey("dbo.TownMerchendisers", "Town_TownId", "dbo.Towns");
            DropIndex("dbo.TownMerchendisers", new[] { "Merchendiser_MerchendiserId" });
            DropIndex("dbo.TownMerchendisers", new[] { "Town_TownId" });
            DropTable("dbo.TownMerchendisers");
            DropTable("dbo.Towns");
            DropTable("dbo.Merchendisers");
        }
    }
}
