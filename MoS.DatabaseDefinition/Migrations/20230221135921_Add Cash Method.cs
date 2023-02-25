using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class AddCashMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE [PaymentOptionTypeDescriptions]
                SET IsDeleted = 0, DeletedAt = NULL, DeletedBy = NULL
                Where Id = 1
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
