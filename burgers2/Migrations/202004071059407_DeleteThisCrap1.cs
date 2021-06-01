namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteThisCrap1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Meals", "pictureUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Meals", "pictureUrl", c => c.String(nullable: false));
        }
    }
}
