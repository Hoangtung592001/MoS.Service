using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class CreateNewExceptionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exceptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExceptionMessageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exceptions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Exceptions",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "Description", "ExceptionMessageType", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("9d4e78aa-919a-415d-8e38-a314aa44a020"), null, null, "Unknown Error", "UNKNOWN", false },
                    { new Guid("d9930df8-6ec6-475b-ad74-2ded0901019c"), null, null, "This user already exists", "USER_FOUND", false },
                    { new Guid("998360fa-14ae-492c-80be-ee28470d8cfa"), null, null, "This user already exists", "USER_NAME_NOT_FOUND", false },
                    { new Guid("88d67bc0-e8c1-41d2-a1fe-7087ccb53e80"), null, null, "Your password is wrong", "WRONG_PASSWORD", false },
                    { new Guid("87c0861e-3641-47a8-87a4-bdd6478e5b65"), null, null, "You are unauthorized now", "UNAUTHORIZED", false },
                    { new Guid("d8acff1b-c89b-4e33-a436-744b362faf70"), null, null, "This author does not exist", "INVALID_AUTHOR", false },
                    { new Guid("c3b9c2d6-8e54-4c74-99bc-94118b24b415"), null, null, "This publisher does not exist", "INVALID_PUBLISHER", false },
                    { new Guid("63a89635-5851-4cd3-bc03-b90f7de409c2"), null, null, "This book must have at least 1 main image", "INVALID_IMAGES", false },
                    { new Guid("472f4bf4-d447-4ffc-992a-85336b89495e"), null, null, "This condition does not exist", "INVALID_CONDITIONS", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exceptions");
        }
    }
}
