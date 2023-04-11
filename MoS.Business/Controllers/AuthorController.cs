using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.AuthorImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.AuthorService;
using MoS.Services.AuthorServices;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Exception;
using static MoS.Services.AuthorServices.GetAuthorService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Author")]
    public class AuthorController : Controller
    {
        private readonly CreateAuthorService.ICreateAuthorService _authorCreator;
        private readonly IApplicationDbContext _db;

        public AuthorController(CreateAuthorService.ICreateAuthorService authorCreator, IApplicationDbContext db)
        {
            _authorCreator = authorCreator;
            _db = db;
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
                        response = Ok(new ExceptionResponse
                        {
                            ExceptionId = exception
                        });
                    }
                );

            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllAuthors()
        {
            return Ok(new BaseResponse<IEnumerable<Author>>
            {
                Success = true,
                Data = new GetAuthorService(new GetAuthorImplementation(_db)).GetAll()
            });
        }

        class CreateAuthorResponse : BaseResponse<DataCreateAuthorResponse>
        {

        }

        class DataCreateAuthorResponse
        {

        }
    }
}
