using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class ChangeDescriptionOfUserNotExist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE [Exceptions]
                SET Description = 'This user does not exist!'
                WHERE Id = '998360FA-14AE-492C-80BE-EE28470D8CFA';
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
