using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class AddTransactionTypeDescriptionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeDescriptionId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TransactionTypeDescriptions",
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
                    table.PrimaryKey("PK_TransactionTypeDescriptions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TransactionTypeDescriptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name" },
                values: new object[] { 1, null, null, "Transaction is pending", false, "Pending" });

            migrationBuilder.InsertData(
                table: "TransactionTypeDescriptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name" },
                values: new object[] { 2, null, null, "Transaction is Succeeded", false, "Succeeded" });

            migrationBuilder.InsertData(
                table: "TransactionTypeDescriptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name" },
                values: new object[] { 3, null, null, "Transaction is Failed", false, "Failed" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeDescriptionId",
                table: "Transactions",
                column: "TransactionTypeDescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTypeDescriptions_TransactionTypeDescriptionId",
                table: "Transactions",
                column: "TransactionTypeDescriptionId",
                principalTable: "TransactionTypeDescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypeDescriptions_TransactionTypeDescriptionId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionTypeDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionTypeDescriptionId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionTypeDescriptionId",
                table: "Transactions");
        }
    }
}
