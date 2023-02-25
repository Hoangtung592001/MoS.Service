using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.CountryImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.CountryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Services.CountryServices.CountryService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Country")]
    public class CountryController : Controller
    {
        private readonly IApplicationDbContext _db;

        public CountryController(IApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Get()
        {
            return Ok(new BaseResponse<IEnumerable<Country>>
            {
                Success = true,
                Data = new CountryService(new CountryImplementation(_db)).Get()
            });
        }
    }
}
