using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class AddPaymentTypeDescriptionsPaymentOptionTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentTypeDescriptions",
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
                    table.PrimaryKey("PK_PaymentTypeDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameOnCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTypeDescriptionId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentOptions_PaymentTypeDescriptions_PaymentTypeDescriptionId",
                        column: x => x.PaymentTypeDescriptionId,
                        principalTable: "PaymentTypeDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentOptions_PaymentOptionId",
                        column: x => x.PaymentOptionId,
                        principalTable: "PaymentOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PaymentTypeDescriptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name" },
                values: new object[] { 1, null, null, "Cash", false, "Cash" });

            migrationBuilder.InsertData(
                table: "PaymentTypeDescriptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name" },
                values: new object[] { 2, null, null, "Visa", false, "Visa" });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentOptions_PaymentTypeDescriptionId",
                table: "PaymentOptions",
                column: "PaymentTypeDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentOptionId",
                table: "Transactions",
                column: "PaymentOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "PaymentOptions");

            migrationBuilder.DropTable(
                name: "PaymentTypeDescriptions");
        }
    }
}
