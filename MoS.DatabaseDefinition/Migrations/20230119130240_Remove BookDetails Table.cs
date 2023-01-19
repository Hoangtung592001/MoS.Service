using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class RemoveBookDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookDetails_BookDetailId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookDetails");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookDetailId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "NumberOfViews",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "BookDetails",
                table: "BookInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfViews",
                table: "BookInformation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookDetails",
                table: "BookInformation");

            migrationBuilder.DropColumn(
                name: "NumberOfViews",
                table: "BookInformation");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfViews",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookDetailId",
                table: "Books",
                column: "BookDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookDetails_BookDetailId",
                table: "Books",
                column: "BookDetailId",
                principalTable: "BookDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
