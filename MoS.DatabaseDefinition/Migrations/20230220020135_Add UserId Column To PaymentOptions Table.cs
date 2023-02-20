using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class AddUserIdColumnToPaymentOptionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PaymentOptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PaymentOptions_UserId",
                table: "PaymentOptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentOptions_Users_UserId",
                table: "PaymentOptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentOptions_Users_UserId",
                table: "PaymentOptions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentOptions_UserId",
                table: "PaymentOptions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentOptions");
        }
    }
}
