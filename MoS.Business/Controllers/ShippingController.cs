using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.ShippingImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.ShippingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Services.ShippingServices.ShippingService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Shipping")]
    public class ShippingController : Controller
    {
        private readonly IApplicationDbContext _db;

        public ShippingController(IApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Authorize]
        [Route("{addressId}")]
        public IActionResult GetShippingFee(Guid addressId)
        {
            return Ok(new BaseResponse<double>
            {
                Success = true,
                Data = new ShippingService(new ShippingImplementation(_db)).Get(addressId)
            });
        }
    }
}
