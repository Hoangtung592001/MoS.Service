using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.ElasticSearchImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.ElasticSearchServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Services.ElasticSearchServices.GetBookService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Search")]
    public class SearchController : Controller
    {
        private readonly IApplicationDbContext _db;
        public IConfiguration _configuration { get; set; }

        public SearchController(IApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> SearchBooks(SearchBookRequest request, int limit)
        {
            return Ok(new BaseResponse<IEnumerable<Book>>
            {
                Success = true,
                Data = await new GetBookService(new GetBookImplementation(_db, _configuration)).Get(request.Title, limit)
            });
        }

        [HttpPost]
        [Route("Whole")]
        public async Task<IActionResult> SearchWholeBooks(SearchBookRequest request, int limit)
        {
            return Ok(new BaseResponse<IEnumerable<WholeBook>>
            {
                Success = true,
                Data = await new GetBookService(new GetBookImplementation(_db, _configuration)).GetWhole(request.Title, limit)
            });
        }
    }
}
