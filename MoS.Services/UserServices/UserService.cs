using MoS.DatabaseDefinition.Models;
using System;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Exception;

namespace MoS.Services.UserServices
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
            public Guid Id { get; set; }
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
            Task UserSignUp(Account user, Action onSucess, Action<Guid> onFail);
            Task UserSignIn(Account user, Action<string> onSucess, Action<Guid> onFail);
            Task VerifyUser(Account user, Action onSucess, Action<Guid> onFail);
        }

        public async Task UserSignIn(UserService.Account user, Action<string> onSucess, Action<Guid> onFail)
        {
            await _repository.UserSignIn(user, onSucess, onFail);
        }

        public async Task UserSignUp(Account user, Action onSucess, Action<Guid> onFail)
        {
            await _repository.UserSignUp(user, onSucess, onFail);
        }
    }
}
