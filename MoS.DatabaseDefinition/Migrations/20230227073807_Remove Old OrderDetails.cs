using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class RemoveOldOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE [OrderDetails]
                SET IsDeleted = 1, DeletedBy = 'BF45FAA4-F2D5-4F45-DC88-08DAFB8D2FAA'
                WHERE Id = '8D87B112-DAB2-40F3-BE86-51A1B14611ED'
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
