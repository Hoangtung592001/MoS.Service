using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Book")]
    public class BookController : Controller
    {
        [HttpGet]
        [Route("FrequentlyViewedItems")]
        public async Task<IActionResult> FrequentlyViewedItems()
        {
            return Ok();
        }
    }
}
