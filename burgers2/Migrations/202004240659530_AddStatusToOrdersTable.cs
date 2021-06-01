namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToOrdersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Status");
        }
    }
}
