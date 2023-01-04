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
        public const string SignInExceptionMessageType = "SignInExceptionMessage";
        public const string AuthenticationExceptionMessageType = "AuthenticationExceptionMessage";
        public const string CreateAuthorExceptionMessageType = "CreateAuthorExceptionMessage";
        
        public enum CreateUserExceptionMessage
        {
            OTHERS = 0,
            USER_FOUND = 1
        }

        public enum SignInExceptionMessage
        {
            OTHERS = 0,
            USER_NAME_NOT_FOUND = 1,
            WRONG_PASSWORD = 2
        }

        public enum AuthenticationExceptionMessage
        {
            OTHERS = 0,
            UNAUTHORIZED = 1
        }

        public enum CreateAuthorExceptionMessage
        {
            OTHERS = 1
        }
    }
}
