using Microsoft.AspNetCore.Authorization;
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
using System.Security.Claims;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Exception;
using static MoS.Services.BookServices.CreateBookService;
using static MoS.Services.BookServices.FrequentlyViewedItemsService;
using static MoS.Services.BookServices.GetBookService;
using static MoS.Services.BookServices.RecentlyViewedItemsService;
using static MoS.Services.BookServices.RecommendedItemsService;

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

        [HttpGet]
        [Route("RecommendedItems")]
        public async Task<IActionResult> RecommendedItems(RecommendedItemsRequest request)
        {
            return Ok(new BaseResponse<IEnumerable<RecommendedItem>>
            {
                Success = true,
                Data = await new RecommendedItemsService(new RecommendedItemsImplementation(_db)).Get(request)
            });
        }

        [Authorize(Roles = "Admin")]
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

        [HttpGet]
        [Route("RecentlyViewedItem")]
        public async Task<IActionResult> GetRecentlyViewedItem(GetRecentlyViewedItemRequest request)
        {
            return Ok(new BaseResponse<IEnumerable<RecentlyViewedItem>>
            {
                Success = true,
                Data = await new RecentlyViewedItemsService(new RecentlyViewedItemsImplementation(_db)).Get(request)
            });
        }

        [HttpPost]
        [Authorize]
        [Route("RecentlyViewedItem")]
        public async Task<IActionResult> SetRecentlyViewedItem(SetRecentlyViewedItemRequest request)
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);
            
            var isCreated = await new RecentlyViewedItemsService(new RecentlyViewedItemsImplementation(_db)).Set(request, credential);

            if (isCreated)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("GetBookDetails/BookId")]
        public async Task<IActionResult> GetBookDetails(Guid BookId)
        {
            return Ok(
                    new BaseResponse<Book>
                    {
                        Success = true,
                        Data = await new GetBookService(new GetBookImplementation(_db)).Get(BookId)
                    }
                );
        }
    }
}
