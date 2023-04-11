using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Models.Constants.Enums
{
    public static class Exception
    {
        public const string CreateUserExceptionMessageType = "CreateUserExceptionMessage";
        public const string UnknownExceptionMessageType = "UnknownExceptionMessage";
        public const string SignInExceptionMessageType = "SignInExceptionMessage";
        public const string AuthenticationExceptionMessageType = "AuthenticationExceptionMessage";
        public const string CreateAuthorExceptionMessageType = "CreateAuthorExceptionMessage";
        public const string CreateBookExceptionMessageType = "CreateBookExceptionMessage";

        public static IDictionary<string, Guid> UnknownExceptionMessages = new Dictionary<string, Guid>() {
            {"UNKNOWN", new Guid("9D4E78AA-919A-415D-8E38-A314AA44A020") }
        };

        public static IDictionary<string, Guid> SignUpExceptionMessages = new Dictionary<string, Guid>() {
            {"USER_FOUND", new Guid("D9930DF8-6EC6-475B-AD74-2DED0901019C") }
        };

        public static IDictionary<string, Guid> SignInExceptionMessages = new Dictionary<string, Guid>() {
            {"USER_NAME_NOT_FOUND", new Guid("998360FA-14AE-492C-80BE-EE28470D8CFA") },
            {"WRONG_PASSWORD", new Guid("88D67BC0-E8C1-41D2-A1FE-7087CCB53E80") }
        };

        public static IDictionary<string, Guid> AuthenticationExceptionMessages = new Dictionary<string, Guid>() {
            {"UNAUTHORIZED", new Guid("87C0861E-3641-47A8-87A4-BDD6478E5B65") }
        };

        public static IDictionary<string, Guid> CreateBookExceptionMessages = new Dictionary<string, Guid>() {
            {"INVALID_AUTHOR", new Guid("D8ACFF1B-C89B-4E33-A436-744B362FAF70") },
            {"INVALID_PUBLISHER", new Guid("C3B9C2D6-8E54-4C74-99BC-94118B24B415") },
            {"INVALID_IMAGES", new Guid("63A89635-5851-4CD3-BC03-B90F7DE409C2") },
            {"INVALID_CONDITIONS", new Guid("472F4BF4-D447-4FFC-992A-85336B89495E") }
        };

        public static IDictionary<string, Guid> ChangeQuantityExceptionMessages = new Dictionary<string, Guid>() {
            {"QUANTITY_EXCEED", new Guid("2DE53BC3-3CC6-494C-8EDD-19484008D39B") },
            {"INVALID_QUANTITY", new Guid("34C4B3D4-8D03-4AE0-85D4-78EE99829621") },
            {"QUANTITY_NOT_AVAILABLE", new Guid("CB18633B-501A-4803-825C-3A68E3BA6D31") },
        };
    }
}
