using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class DeleteBookInformationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookInformation_BookInformationId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookInformation");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookInformationId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookInformationId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "BookConditionId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "BookDetails",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Edition",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfViews",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Books",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SellOffRate",
                table: "Books",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookConditionId",
                table: "Books",
                column: "BookConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookConditions_BookConditionId",
                table: "Books",
                column: "BookConditionId",
                principalTable: "BookConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookConditions_BookConditionId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookConditionId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookConditionId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookDetails",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "NumberOfViews",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "SellOffRate",
                table: "Books");

            migrationBuilder.AddColumn<Guid>(
                name: "BookInformationId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BookInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookConditionId = table.Column<int>(type: "int", nullable: false),
                    BookDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Edition = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfViews = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SellOffRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookInformation_BookConditions_BookConditionId",
                        column: x => x.BookConditionId,
                        principalTable: "BookConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookInformationId",
                table: "Books",
                column: "BookInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInformation_BookConditionId",
                table: "BookInformation",
                column: "BookConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookInformation_BookInformationId",
                table: "Books",
                column: "BookInformationId",
                principalTable: "BookInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
