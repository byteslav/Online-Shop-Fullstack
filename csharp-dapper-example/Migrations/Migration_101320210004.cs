using FluentMigrator;

namespace csharp_dapper_example.Migrations
{
    [Migration(101320210004)]
    public class Migration_101320210004 : Migration
    {
        public override void Up()
        {
            Create.Table("products")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("count").AsInt32().NotNullable()
                .WithColumn("price").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("products");
        }
    }
}