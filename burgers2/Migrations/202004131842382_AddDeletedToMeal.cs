namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletedToMeal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meals", "Deleted");
        }
    }
}
