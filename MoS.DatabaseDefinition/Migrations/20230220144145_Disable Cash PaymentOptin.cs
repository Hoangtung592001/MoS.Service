using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class DisableCashPaymentOptin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE [PaymentOptionTypeDescriptions]
                SET IsDeleted = 1, DeletedAt = GetDate(), DeletedBy = 'BF45FAA4-F2D5-4F45-DC88-08DAFB8D2FAA'
                Where Id = 1
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
