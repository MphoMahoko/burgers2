namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReservationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        NumberPeople = c.String(),
                        Date = c.DateTime(nullable: false),
                        Time = c.String(),
                        Venue = c.String(),
                        Message = c.String(),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "Customer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reservations", new[] { "Customer_Id" });
            DropTable("dbo.Reservations");
        }
    }
}
