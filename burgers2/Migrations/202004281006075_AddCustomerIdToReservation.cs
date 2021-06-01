namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerIdToReservation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reservations", name: "Customer_Id", newName: "CustomerId");
            RenameIndex(table: "dbo.Reservations", name: "IX_Customer_Id", newName: "IX_CustomerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Reservations", name: "IX_CustomerId", newName: "IX_Customer_Id");
            RenameColumn(table: "dbo.Reservations", name: "CustomerId", newName: "Customer_Id");
        }
    }
}
