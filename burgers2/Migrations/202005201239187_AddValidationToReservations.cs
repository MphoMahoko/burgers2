namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationToReservations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Reservations", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Reservations", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Reservations", "Message", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "Message", c => c.String());
            AlterColumn("dbo.Reservations", "Email", c => c.String());
            AlterColumn("dbo.Reservations", "Phone", c => c.String());
            AlterColumn("dbo.Reservations", "Name", c => c.String());
        }
    }
}
