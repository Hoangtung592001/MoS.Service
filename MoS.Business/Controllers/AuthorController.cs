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
                        response = BadRequest(new ExceptionResponse
                        {
                            ExceptionId = exception
                        });
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
