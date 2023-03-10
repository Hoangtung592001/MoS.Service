using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.BookImplementations;
using MoS.Implementations.CommonImplementations;
using MoS.Implementations.ElasticSearchImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.BookServices;
using MoS.Services.CommonServices;
using MoS.Services.ElasticSearchServices;
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
using static MoS.Services.ElasticSearchServices.DeleteBookService;
using static MoS.Services.ElasticSearchServices.SetBookService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Book")]
    public class BookController : Controller
    {
        private readonly IApplicationDbContext _db;
        private readonly ISetBook _elasticSetBookService;
        private readonly IDeleteBook _elasticDeleteBookService;

        public BookController(IApplicationDbContext db, ISetBook elasticSetBookService, IDeleteBook elasticDeleteBookService)
        {
            _db = db;
            _elasticSetBookService = elasticSetBookService;
            _elasticDeleteBookService = elasticDeleteBookService;
        }

        [HttpPost]
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
                new CreateBookImplementation(_db, new CommonImplementation(_db), _elasticSetBookService))
                .Create(
                request,
                () => {
                    response = Ok();
                },
                (exception) => {
                    response = Ok(new ExceptionResponse
                    {
                        ExceptionId = exception
                    });
                });

            return response;
        }

        [HttpGet]
        [Route("RecentlyViewedItems")]
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
        [Route("GetBookDetails/{BookId}")]
        public async Task<IActionResult> GetBookDetails(Guid BookId)
        {
            return Ok(
                    new BaseResponse<Book>
                    {
                        Success = true,
                        Data = await new Services.BookServices.GetBookService(new Implementations.BookImplementations.GetBookImplementation(_db)).Get(BookId)
                    }
                );
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("DeleteBook/{BookId}")]
        public async Task<IActionResult> DeleteBook(Guid BookId)
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            var isDeleted = await new Services.BookServices.DeleteBookService(new Implementations.BookImplementations.DeleteBookImplementation(_db, _elasticDeleteBookService)).Delete(BookId, credential);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("SyncBookToElasticSearch")]
        public async Task<IActionResult> SyncBookToElasticSearch()
        {
            await new SyncBooksToElasticSearchService(new SyncBooksToElasticSearchImplementation(_db, _elasticSetBookService)).Sync();

            return Ok();
        }
    }
}
