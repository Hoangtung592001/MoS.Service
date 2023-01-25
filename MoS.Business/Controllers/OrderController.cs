using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.CommonImplementations;
using MoS.Implementations.OrderImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.CommonServices;
using MoS.Services.OrderServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Services.OrderServices.OrderService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly IApplicationDbContext _db;

        public OrderController(IApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            return Ok(
                    new BaseResponse<IEnumerable<Order>>
                    {
                        Success = true,
                        Data = await new OrderService(new OrderImplementation(_db)).Get(credential)
                    }
                );
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SetOrder(SetOrderRequest request)
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            var isOrderCreated = await new OrderService(new OrderImplementation(_db)).Set(request, credential);


            if (!isOrderCreated)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
