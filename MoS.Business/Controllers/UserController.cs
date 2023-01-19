using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using MoS.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Exception;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IApplicationDbContext _db;
        private readonly UserService.IUserService _userService;
        private readonly HashService.IHashService _hashService;
        private readonly TokenService.ITokenService _tokenService;

        public UserController(IApplicationDbContext db,
                            UserService.IUserService userService,
                            HashService.IHashService hashService,
                            TokenService.ITokenService tokenService)
        {
            _db = db;
            _hashService = hashService;
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(UserService.Account user)
        {
            IActionResult response = null;

            await _userService.UserSignUp(
                user,
                () =>
                {
                    response = Ok(new SignUpResponse
                    {
                        Success = true
                    });
                },
                (exception) =>
                {
                    if (exception == CreateUserExceptionMessage.USER_FOUND)
                    {
                        response = BadRequest(new ExceptionResponse
                        {
                            ErrorType = CreateUserExceptionMessageType,
                            ErrorMessage = (int)CreateUserExceptionMessage.USER_FOUND
                        });
                    }
                    else
                    {
                        response = BadRequest(new ExceptionResponse
                        {
                            ErrorType = CreateUserExceptionMessageType,
                            ErrorMessage = (int)CreateUserExceptionMessage.OTHERS
                        });
                    }
                }
                );

            return response;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(UserService.Account user)
        {
            IActionResult response = null;

            await _userService.UserSignIn(
                    user,
                    (token) =>
                    {
                        response = Ok(new SignInResponse
                        {
                            Success = true,
                            Data = new DataSignInResponse
                            {
                                Token = token
                            }
                        });
                    },
                    (exception) =>
                    {
                        if (exception == SignInExceptionMessage.USER_NAME_NOT_FOUND)
                        {
                            response = BadRequest(new ExceptionResponse
                            {
                                ErrorType = SignInExceptionMessageType,
                                ErrorMessage = (int)SignInExceptionMessage.USER_NAME_NOT_FOUND
                            });
                        }
                        else if (exception == SignInExceptionMessage.WRONG_PASSWORD)
                        {
                            response = BadRequest(new ExceptionResponse
                            {
                                ErrorType = SignInExceptionMessageType,
                                ErrorMessage = (int)SignInExceptionMessage.WRONG_PASSWORD
                            });
                        }
                        else
                        {
                            response = BadRequest(new ExceptionResponse
                            {
                                ErrorType = SignInExceptionMessageType,
                                ErrorMessage = (int)SignInExceptionMessage.OTHERS
                            });
                        }
                    });

            return response;
        }

        class SignUpResponse : BaseResponse<DataSignUpResponse>
        {

        }

        class DataSignUpResponse
        {

        }

        class SignInResponse : BaseResponse<DataSignInResponse>
        {

        }

        class DataSignInResponse
        {
            public string Token { get; set; }
        }
    }
}
