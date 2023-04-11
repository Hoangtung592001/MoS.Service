using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class AdddataforchangQuantityErrorMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exceptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "ExceptionMessageType", "IsDeleted" },
                values: new object[] { new Guid("2de53bc3-3cc6-494c-8edd-19484008d39b"), null, null, "The selected quantity exceeds quantity avaiable in stock.", "QUANTITY_EXCEED", false });

            migrationBuilder.InsertData(
                table: "Exceptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "ExceptionMessageType", "IsDeleted" },
                values: new object[] { new Guid("34c4b3d4-8d03-4ae0-85d4-78ee99829621"), null, null, "The selected quantity is invalid.", "INVALID_QUANTITY", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exceptions",
                keyColumn: "Id",
                keyValue: new Guid("2de53bc3-3cc6-494c-8edd-19484008d39b"));

            migrationBuilder.DeleteData(
                table: "Exceptions",
                keyColumn: "Id",
                keyValue: new Guid("34c4b3d4-8d03-4ae0-85d4-78ee99829621"));
        }
    }
}
