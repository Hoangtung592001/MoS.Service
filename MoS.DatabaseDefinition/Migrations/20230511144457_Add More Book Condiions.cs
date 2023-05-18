using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class AddMoreBookCondiions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookConditions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "IsDeleted", "Name" },
                values: new object[] { 2, null, null, false, "New" });

            migrationBuilder.InsertData(
                table: "BookConditions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "IsDeleted", "Name" },
                values: new object[] { 3, null, null, false, "Old" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookConditions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookConditions",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
