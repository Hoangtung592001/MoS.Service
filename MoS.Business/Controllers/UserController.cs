using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using MoS.Services.UserServices;
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
                (exception) => {
                    response = BadRequest(new ExceptionResponse
                    {
                        ExceptionId = exception
                    });
                });

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
                    (exception) => {
                        response = BadRequest(new ExceptionResponse
                        {
                            ExceptionId = exception
                        });
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
