using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class UpdateDeletedByAndDeletedAtNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Update Books
                                SET DeletedAt = NULL,
                                DeletedBy = NULL
                                WHERE Id IN('8D87B112-DAB2-40F3-BE86-51A1B14611ED', '6DFF6057-EF06-4F5B-89E1-576C97B49375', '6D20F9D5-44BD-467A-ABF4-59A0404BDAF9', 'BCD92667-27B9-490D-93C3-F4627F009932');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
