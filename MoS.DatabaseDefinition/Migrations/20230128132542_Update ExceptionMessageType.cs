using Microsoft.EntityFrameworkCore.Migrations;

namespace MoS.DatabaseDefinition.Migrations
{
    public partial class UpdateExceptionMessageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Exceptions
                SET ExceptionMessageType = 'Unknown'
                WHERE Id = '9D4E78AA-919A-415D-8E38-A314AA44A020';

                UPDATE Exceptions
                SET ExceptionMessageType = 'SignUp'
                WHERE Id = 'D9930DF8-6EC6-475B-AD74-2DED0901019C';

                UPDATE Exceptions
                SET ExceptionMessageType = 'SignIn'
                WHERE Id IN('88D67BC0-E8C1-41D2-A1FE-7087CCB53E80', '998360FA-14AE-492C-80BE-EE28470D8CFA');

                UPDATE Exceptions
                SET ExceptionMessageType = 'Authentication'
                WHERE Id IN('87C0861E-3641-47A8-87A4-BDD6478E5B65');

                UPDATE Exceptions
                SET ExceptionMessageType = 'CreateBook'
                WHERE Id IN('D8ACFF1B-C89B-4E33-A436-744B362FAF70', '472F4BF4-D447-4FFC-992A-85336B89495E', 'C3B9C2D6-8E54-4C74-99BC-94118B24B415', '63A89635-5851-4CD3-BC03-B90F7DE409C2');
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
