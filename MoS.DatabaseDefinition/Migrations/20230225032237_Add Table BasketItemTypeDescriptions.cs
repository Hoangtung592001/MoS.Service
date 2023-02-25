using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class AddTableBasketItemTypeDescriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasketItemTypeDescriptionId",
                table: "BasketItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BasketItemTypeDescriptions",
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
                    table.PrimaryKey("PK_BasketItemTypeDescriptions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BasketItemTypeDescriptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name" },
                values: new object[] { 1, null, null, "Item is still in basket", false, "In Basket" });

            migrationBuilder.InsertData(
                table: "BasketItemTypeDescriptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "IsDeleted", "Name" },
                values: new object[] { 2, null, null, "Item is ordered", false, "Ordered" });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketItemTypeDescriptionId",
                table: "BasketItems",
                column: "BasketItemTypeDescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_BasketItemTypeDescriptions_BasketItemTypeDescriptionId",
                table: "BasketItems",
                column: "BasketItemTypeDescriptionId",
                principalTable: "BasketItemTypeDescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_BasketItemTypeDescriptions_BasketItemTypeDescriptionId",
                table: "BasketItems");

            migrationBuilder.DropTable(
                name: "BasketItemTypeDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_BasketItemTypeDescriptionId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "BasketItemTypeDescriptionId",
                table: "BasketItems");
        }
    }
}
