namespace MerchendisersMVC.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AfterValidatingAttribute : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Merchendisers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Merchendisers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Merchendisers", "Address_Street", c => c.String(nullable: false));
            AlterColumn("dbo.Merchendisers", "Address_PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.Merchendisers", "Address_City", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Merchendisers", "Address_City", c => c.String());
            AlterColumn("dbo.Merchendisers", "Address_PostalCode", c => c.String());
            AlterColumn("dbo.Merchendisers", "Address_Street", c => c.String());
            AlterColumn("dbo.Merchendisers", "LastName", c => c.String());
            AlterColumn("dbo.Merchendisers", "FirstName", c => c.String());
        }
    }
}
