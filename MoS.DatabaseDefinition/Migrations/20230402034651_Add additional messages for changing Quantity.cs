using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class AddadditionalmessagesforchangingQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Exceptions",
                keyColumn: "Id",
                keyValue: new Guid("2de53bc3-3cc6-494c-8edd-19484008d39b"),
                column: "Description",
                value: "The selected quantity exceeds quantity available in stock.");

            migrationBuilder.InsertData(
                table: "Exceptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "ExceptionMessageType", "IsDeleted" },
                values: new object[] { new Guid("cb18633b-501a-4803-825c-3a68e3ba6d31"), null, null, "The selected basket item is not available.", "QUANTITY_NOT_AVAILABLE", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exceptions",
                keyColumn: "Id",
                keyValue: new Guid("cb18633b-501a-4803-825c-3a68e3ba6d31"));

            migrationBuilder.UpdateData(
                table: "Exceptions",
                keyColumn: "Id",
                keyValue: new Guid("2de53bc3-3cc6-494c-8edd-19484008d39b"),
                column: "Description",
                value: "The selected quantity exceeds quantity avaiable in stock.");
        }
    }
}
