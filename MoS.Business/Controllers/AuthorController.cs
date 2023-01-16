using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoS.Models.CommonUseModels;
using MoS.Services.AuthorService;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Exception;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Author")]
    public class AuthorController : Controller
    {
        private readonly CreateAuthorService.ICreateAuthorService _authorCreator;

        public AuthorController(CreateAuthorService.ICreateAuthorService authorCreator)
        {
            _authorCreator = authorCreator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("CreateNewAuthor")]
        public async Task<IActionResult> CreateNewAuthor(CreateAuthorService.Author author)
        {
            string userId = User.FindFirst(ClaimTypes.Name)?.Value;
            string role = User.FindFirst(ClaimTypes.Role)?.Value;

            IActionResult response = null;
            await new CreateAuthorService(_authorCreator).Create(
                    author,
                    () => {
                        response = Ok(new CreateAuthorResponse
                        {
                            Success = true
                        });
                    },
                    (exception) => {
                        if (exception == CreateAuthorExceptionMessage.OTHERS)
                        {
                            response = BadRequest(new ExceptionResponse
                            {
                                ErrorType = CreateAuthorExceptionMessageType,
                                ErrorMessage = (int)CreateAuthorExceptionMessage.OTHERS
                            });
                        }
                    }
                );

            return response;
        }

        class CreateAuthorResponse : BaseResponse<DataCreateAuthorResponse>
        {

        }

        class DataCreateAuthorResponse
        {

        }
    }
}
