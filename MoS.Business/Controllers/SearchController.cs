using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.SearchBookImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.SearchBookServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Services.SearchBookServices.SearchBookService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Search")]
    public class SearchController : Controller
    {
        private readonly IApplicationDbContext _db;

        public SearchController(IApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> GetShippingFee(SearchBookRequest request)
        {
            return Ok(new BaseResponse<IEnumerable<Book>>
            {
                Success = true,
                Data = await new SearchBookService(new SearchBookImplementaion(_db)).Get(request.Title)
            });
        }
    }
}
