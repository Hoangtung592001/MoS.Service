using MoS.DatabaseDefinition.Models;
using System;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Exception;

namespace MoS.Services.AuthenticationService
{
    public class UserService
    {
        IUserService _repository;

        public UserService(IUserService repository)
        {
            _repository = repository;
        }

        public class FoundUser
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public Role Role { get; set; }
        }

        public class Account
        {
            public string Username { get; set; }
            public string Passoword { get; set; }
        }

        public class TokenData
        {
            public string Token { get; set; }
        }

        public interface IUserService
        {
            Task GetUserInfo(string username, Action<FoundUser> onItemReturn, Action onNotFound);
            Task UserSignUp(Account user, Action onSucess, Action<CreateUserExceptionMessage> onFail);
            Task UserSignIn(Account user, Action<string> onSucess, Action<SignInExceptionMessage> onFail);
            Task VerifyUser(Account user, Action onSucess, Action<SignInExceptionMessage> onFail);
        }

        public async Task UserSignIn(UserService.Account user, Action<string> onSucess, Action<SignInExceptionMessage> onFail)
        {
            await _repository.UserSignIn(user, onSucess, onFail);
        }

        public async Task UserSignUp(Account user, Action onSucess, Action<CreateUserExceptionMessage> onFail)
        {
            await _repository.UserSignUp(user, onSucess, onFail);
        }
    }
}
