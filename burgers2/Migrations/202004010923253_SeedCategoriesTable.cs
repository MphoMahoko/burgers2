namespace burgers2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedCategoriesTable : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Categories (Name) values ('burger')");
            Sql("insert into Categories (Name) values ('chips')");
            Sql("insert into Categories (Name) values ('salad')");
            Sql("insert into Categories (Name) values ('dessert')");
        }
        
        public override void Down()
        {
            Sql("delete from Categories where Id in (1, 2, 3, 4)");

        }
    }
}
