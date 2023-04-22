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
using static MoS.Services.OrderServices.GenerateOrderNumberService;
using static MoS.Services.OrderServices.OrderService;
using static MoS.Services.ShippingServices.ShippingService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly IApplicationDbContext _db;
        private readonly IShipping _shippingService;
        private readonly IGenerateOrderNumber _generateOrderNumberService;

        public OrderController(IApplicationDbContext db, IShipping shippingService, IGenerateOrderNumber generateOrderNumberService)
        {
            _db = db;
            _shippingService = shippingService;
            _generateOrderNumberService = generateOrderNumberService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetOrders()
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            return Ok(
                    new BaseResponse<IEnumerable<Order>>
                    {
                        Success = true,
                        Data = new OrderService(new OrderImplementation(_db)).Get(credential)
                    }
                );
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SetOrder(SetOrderRequest request)
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);
            IActionResult response = null;

            await new OrderService(new OrderImplementation(_db, _shippingService, _generateOrderNumberService))
                    .Set(request, 
                        credential,
                        (orderId) =>
                        {
                            response = Ok(new BaseResponse<string>
                            {
                                Success = true,
                                Data = orderId
                            });
                        },
                        (exceptionId) => {
                            response = Ok(new ExceptionResponse
                            {
                                ExceptionId = exceptionId
                            });
                        });


            return response;
        }
    }
}
