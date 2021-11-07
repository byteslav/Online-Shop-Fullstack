using System.Data;
using FluentMigrator;

namespace CsharpDapperExample.Migrations
{
    [Migration(202111030002)]
     public class AddTablesMigration_202111030002 : Migration
     {
         public override void Up()
         {
             Create.Table("category")
                 .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                 .WithColumn("name").AsString();
             Create.Table("products")
                 .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                 .WithColumn("name").AsString().NotNullable()
                 .WithColumn("price").AsInt32().NotNullable()
                 .WithColumn("description").AsString().NotNullable()
                 .WithColumn("categoryid").AsInt32().ForeignKey("category", "id").OnDelete(Rule.Cascade);
         }
    
         public override void Down()
         {
             Delete.Table("products");
             Delete.Table("category");
         }
     }
}