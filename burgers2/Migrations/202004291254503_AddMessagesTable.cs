namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessagesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Subject = c.String(),
                        TheMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Messages");
        }
    }
}
