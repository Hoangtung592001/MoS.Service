using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Services.AddressServices.SetAddressService;
using static MoS.Services.AddressServices.GetAddressService;
using MoS.Implementations.CommonImplementations;
using MoS.Services.CommonServices;
using MoS.Services.AddressServices;
using MoS.Implementations.AddressImplementations;
using MoS.Models.CommonUseModels;

namespace MoS.Business.Controllers
{

    [ApiController]
    [Route("address")]
    public class AddressController : Controller
    {
        private readonly IApplicationDbContext _db;

        public AddressController(IApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SetAddress(Services.AddressServices.SetAddressService.Address address)
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            var isSet = await new SetAddressService(new SetAddressImplementation(_db)).Set(credential, address);

            if (!isSet)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAddress()
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            return Ok(new BaseResponse<IEnumerable<Services.AddressServices.GetAddressService.Address>>
            {
                Success = true,
                Data = new GetAddressService(new GetAddressImplementation(_db)).Get(credential)
            });
        }
    }
}
