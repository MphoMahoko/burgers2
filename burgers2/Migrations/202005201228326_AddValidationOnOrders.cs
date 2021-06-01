namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationOnOrders : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Firstname", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Lastname", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Address1", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Email", c => c.String());
            AlterColumn("dbo.Orders", "Phone", c => c.String());
            AlterColumn("dbo.Orders", "Address1", c => c.String());
            AlterColumn("dbo.Orders", "Lastname", c => c.String());
            AlterColumn("dbo.Orders", "Firstname", c => c.String());
        }
    }
}
