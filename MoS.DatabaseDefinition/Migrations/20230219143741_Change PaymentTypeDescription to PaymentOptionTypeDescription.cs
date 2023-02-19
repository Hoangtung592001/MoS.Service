using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class ChangePaymentTypeDescriptiontoPaymentOptionTypeDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentOptions_PaymentTypeDescriptions_PaymentTypeDescriptionId",
                table: "PaymentOptions");

            migrationBuilder.DropTable(
                name: "PaymentTypeDescriptions");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeDescriptionId",
                table: "PaymentOptions",
                newName: "PaymentOptionTypeDescriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentOptions_PaymentTypeDescriptionId",
                table: "PaymentOptions",
                newName: "IX_PaymentOptions_PaymentOptionTypeDescriptionId");

            migrationBuilder.CreateTable(
                name: "PaymentOptionTypeDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOptionTypeDescriptions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PaymentOptionTypeDescriptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name" },
                values: new object[] { 1, null, null, "Cash", false, "Cash" });

            migrationBuilder.InsertData(
                table: "PaymentOptionTypeDescriptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name" },
                values: new object[] { 2, null, null, "Visa", false, "Visa" });

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentOptions_PaymentOptionTypeDescriptions_PaymentOptionTypeDescriptionId",
                table: "PaymentOptions",
                column: "PaymentOptionTypeDescriptionId",
                principalTable: "PaymentOptionTypeDescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentOptions_PaymentOptionTypeDescriptions_PaymentOptionTypeDescriptionId",
                table: "PaymentOptions");

            migrationBuilder.DropTable(
                name: "PaymentOptionTypeDescriptions");

            migrationBuilder.RenameColumn(
                name: "PaymentOptionTypeDescriptionId",
                table: "PaymentOptions",
                newName: "PaymentTypeDescriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentOptions_PaymentOptionTypeDescriptionId",
                table: "PaymentOptions",
                newName: "IX_PaymentOptions_PaymentTypeDescriptionId");

            migrationBuilder.CreateTable(
                name: "PaymentTypeDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypeDescriptions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PaymentTypeDescriptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name" },
                values: new object[] { 1, null, null, "Cash", false, "Cash" });

            migrationBuilder.InsertData(
                table: "PaymentTypeDescriptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name" },
                values: new object[] { 2, null, null, "Visa", false, "Visa" });

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentOptions_PaymentTypeDescriptions_PaymentTypeDescriptionId",
                table: "PaymentOptions",
                column: "PaymentTypeDescriptionId",
                principalTable: "PaymentTypeDescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
