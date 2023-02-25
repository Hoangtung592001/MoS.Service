using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class NewPaymenOptionCash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentOptions_Users_UserId",
                table: "PaymentOptions");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "PaymentOptions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "PaymentOptions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "PaymentOptions",
                columns: new[] { "Id", "CardNumber", "DeletedAt", "DeletedBy", "ExpiryDate", "IsDeleted", "NameOnCard", "PaymentOptionTypeDescriptionId", "UserId" },
                values: new object[] { new Guid("e3037927-7a79-43a9-91b2-f788406436a6"), null, null, null, null, false, null, 1, null });

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentOptions_Users_UserId",
                table: "PaymentOptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentOptions_Users_UserId",
                table: "PaymentOptions");

            migrationBuilder.DeleteData(
                table: "PaymentOptions",
                keyColumn: "Id",
                keyValue: new Guid("e3037927-7a79-43a9-91b2-f788406436a6"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "PaymentOptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "PaymentOptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentOptions_Users_UserId",
                table: "PaymentOptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
