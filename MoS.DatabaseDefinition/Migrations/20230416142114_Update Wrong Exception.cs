using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class UpdateWrongException : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE exceptions SET Description = 'This user not found' WHERE (Id = '998360fa-14ae-492c-80be-ee28470d8cfa')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
