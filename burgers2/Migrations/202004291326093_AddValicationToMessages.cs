namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValicationToMessages : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Messages", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Messages", "Subject", c => c.String(nullable: false));
            AlterColumn("dbo.Messages", "TheMessage", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "TheMessage", c => c.String());
            AlterColumn("dbo.Messages", "Subject", c => c.String());
            AlterColumn("dbo.Messages", "Email", c => c.String());
            AlterColumn("dbo.Messages", "Name", c => c.String());
        }
    }
}
