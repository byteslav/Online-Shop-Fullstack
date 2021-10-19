using FluentMigrator;

namespace CsharpDapperExample.Migrations
{
    [Migration(202110190001)]
    public class Migration_202110190001 : Migration
    {
        public override void Up()
        {
            Create.Table("category")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString().NotNullable();
            Create.Table("products")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("count").AsInt32().NotNullable()
                .WithColumn("price").AsInt32().NotNullable()
                .WithColumn("categoryId").AsInt32().ForeignKey("category", "id")
                .WithColumn("category").AsString();
        }

        public override void Down()
        {
            Delete.Table("products");
            Delete.Table("category");
        }
    }
}