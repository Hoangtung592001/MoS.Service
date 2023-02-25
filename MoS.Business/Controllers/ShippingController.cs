using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        [Authorize]
        public IActionResult GetShippingFee(ShippingRequest shippingRequest)
        {
            return Ok(new BaseResponse<double>
            {
                Success = true,
                Data = new ShippingService(new ShippingImplementation()).Get(shippingRequest.Distance)
            });
        }
    }
}
