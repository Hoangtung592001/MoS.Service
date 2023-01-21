using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using MoS.DatabaseDefinition.Models;
using MoS.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Exception;
using static MoS.Models.Constants.Enums.Role;
using static MoS.Services.UserServices.UserService;

namespace MoS.Implementations.AuthenticationImplementations
{
    public class UserImplementation : IUserService
    {
        private readonly IApplicationDbContext _repository;
        private readonly HashService.IHashService _hashService;
        private readonly TokenService.ITokenService _tokenService;
        public UserImplementation(ApplicationDbContext repository,
                            HashService.IHashService hashService,
                            TokenService.ITokenService tokenService)
        {
            _repository = repository;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public async Task UserSignUp(UserService.Account user, Action onSucess, Action<CreateUserExceptionMessage> onFail)
        {
            bool isUserFound = true;
            await GetUserInfo(user.Username,
                            (userInDatabase) => {
                                isUserFound = false;
                                onFail(CreateUserExceptionMessage.USER_FOUND);
                            },
                            () => {
                                onSucess();
                            });
            if (!isUserFound)
            {
                return;
            }

            string hashPassword = _hashService.Hash(user.Passoword);

            var newUser = new User
            {
                Username = user.Username,
                Password = hashPassword,
                RoleId = (int)RoleIDs.User
            };

            await _repository.Users.AddAsync(newUser);
            await _repository.SaveChangesAsync();
        }

        public async Task UserSignIn(UserService.Account user, Action<string> onSucess, Action<SignInExceptionMessage> onFail)
        {
            bool isVerified = true;
            await VerifyUser(
                    user,
                    () =>
                    { },
                    (exception) =>
                    {
                        onFail(exception);
                        isVerified = false;
                    });

            if (!isVerified)
            {
                return;
            }

            await GetUserInfo(
                    user.Username,
                    (foundUser) =>
                    {
                        var token = _tokenService.BuildToken(new TokenService.TokenProps
                        {
                            Id = foundUser.Id,
                            Role = foundUser.Role.Name
                        });

                        onSucess(token);
                    },
                    () =>
                    {
                        onFail(SignInExceptionMessage.USER_NAME_NOT_FOUND);
                    });
        }

        public async Task VerifyUser(UserService.Account user, Action onSucess, Action<SignInExceptionMessage> onFail)
        {
            await GetUserInfo(
                    user.Username,
                    (foundUser) =>
                    {
                        bool isVerified = _hashService.Verify(foundUser.Password, user.Passoword);

                        if (!isVerified)
                        {
                            onFail(SignInExceptionMessage.WRONG_PASSWORD);
                            return;
                        }

                        onSucess();
                    },
                    () =>
                    {
                        onFail(SignInExceptionMessage.USER_NAME_NOT_FOUND);
                    });
        }

        public async Task GetUserInfo(string username, Action<UserService.FoundUser> onItemReturn, Action onNotFound)
        {
            var entry = (await _repository.Users.Include(user => user.Role).Select(user => new FoundUser
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Role = user.Role
            }).Where(user => user.Username == username).ToListAsync()).FirstOrDefault();

            if (entry == null)
            {
                onNotFound();
                return;
            }

            onItemReturn(entry);
        }
    }
}
