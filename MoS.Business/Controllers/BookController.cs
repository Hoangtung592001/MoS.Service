using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.BookImplementations;
using MoS.Implementations.CommonImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.BookServices;
using MoS.Services.CommonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Exception;
using static MoS.Services.BookServices.CreateBookService;
using static MoS.Services.BookServices.FrequentlyViewedItemsService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Book")]
    public class BookController : Controller
    {
        private readonly IApplicationDbContext _db;

        public BookController(IApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("FrequentlyViewedItems")]
        public async Task<IActionResult> FrequentlyViewedItems(FrequentlyViewedItemsRequest request)
        {
            return Ok(new BaseResponse<IEnumerable<FrequentlyViewedItem>> {
                Success = true,
                Data = await new FrequentlyViewedItemsService(new FrequentlyViewedItemsImplementation(_db)).Get(request)
            });
        }

        [HttpPost]
        [Route("CreateBook")]
        public async Task<IActionResult> CreateBook(CreateBookRequest request)
        {
            IActionResult response = null;

            await new CreateBookService(
                new CreateBookImplementation(_db, new CommonImplementation(_db)))
                .Create(
                request,
                () => {
                    response = Ok();
                },
                (exception) =>
                {
                    if (exception == CreateBookExceptionMessage.INVALID_AUTHOR)
                    {
                        response = BadRequest(new ExceptionResponse
                        {
                            ErrorType = CreateBookExceptionMessageType,
                            ErrorMessage = (int) CreateBookExceptionMessage.INVALID_AUTHOR
                        });
                    }
                    else if (exception == CreateBookExceptionMessage.INVALID_IMAGES)
                    {
                        response = BadRequest(new ExceptionResponse
                        {
                            ErrorType = CreateBookExceptionMessageType,
                            ErrorMessage = (int) CreateBookExceptionMessage.INVALID_IMAGES
                        });
                    }
                    else if (exception == CreateBookExceptionMessage.INVALID_PUBLISHER)
                    {
                        response = BadRequest(new ExceptionResponse
                        {
                            ErrorType = CreateBookExceptionMessageType,
                            ErrorMessage = (int)CreateBookExceptionMessage.INVALID_PUBLISHER
                        });
                    }
                    else if (exception == CreateBookExceptionMessage.INVALID_CONDITIONS)
                    {
                        response = BadRequest(new ExceptionResponse
                        {
                            ErrorType = CreateBookExceptionMessageType,
                            ErrorMessage = (int)CreateBookExceptionMessage.INVALID_CONDITIONS
                        });
                    }
                });

            return response;
        }
    }
}
