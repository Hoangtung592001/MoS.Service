using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class AddColumnSyncToElasticToBooksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SyncToElastic",
                table: "Books",
                type: "bit",
                nullable: true);

            migrationBuilder.Sql(@"
                Update [Books]
                SET SyncToElastic = 0
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SyncToElastic",
                table: "Books");
        }
    }
}
