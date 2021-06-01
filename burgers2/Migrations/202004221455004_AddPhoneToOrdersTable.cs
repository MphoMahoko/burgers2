namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneToOrdersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Phone");
        }
    }
}
