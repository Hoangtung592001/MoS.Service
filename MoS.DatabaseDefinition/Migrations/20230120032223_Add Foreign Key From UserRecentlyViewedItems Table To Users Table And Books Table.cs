using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class AddForeignKeyFromUserRecentlyViewedItemsTableToUsersTableAndBooksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserRecentlyViewedItems_BookId",
                table: "UserRecentlyViewedItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRecentlyViewedItems_UserId",
                table: "UserRecentlyViewedItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecentlyViewedItems_Books_BookId",
                table: "UserRecentlyViewedItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecentlyViewedItems_Users_UserId",
                table: "UserRecentlyViewedItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecentlyViewedItems_Books_BookId",
                table: "UserRecentlyViewedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRecentlyViewedItems_Users_UserId",
                table: "UserRecentlyViewedItems");

            migrationBuilder.DropIndex(
                name: "IX_UserRecentlyViewedItems_BookId",
                table: "UserRecentlyViewedItems");

            migrationBuilder.DropIndex(
                name: "IX_UserRecentlyViewedItems_UserId",
                table: "UserRecentlyViewedItems");
        }
    }
}
