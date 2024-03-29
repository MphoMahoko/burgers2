namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPictureURLToMeal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "pictureUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meals", "pictureUrl");
        }
    }
}
