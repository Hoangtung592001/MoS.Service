using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.AddressImplementations;
using MoS.Implementations.CommonImplementations;
using MoS.Implementations.PaymentImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.CommonServices;
using MoS.Services.PaymentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Services.PaymentServices.SetPaymentOptionsService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Payment")]
    public class PaymentController : Controller
    {
        private readonly IApplicationDbContext _db;

        public PaymentController(IApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Authorize]
        [Route("PaymentOption")]
        public async Task<IActionResult> SetPaymentOption(PaymentOption paymentOption)
        {
            IActionResult response = null;
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            await new SetPaymentOptionsService(new SetPaymentOptionsImplementation(_db))
                .Set(
                    credential, 
                    paymentOption, 
                    (paymentOptionId) => {
                        response = Ok(new BaseResponse<Guid>
                        {
                            Success = true,
                            Data = paymentOptionId
                        });
                    }, 
                    () => {
                        response = BadRequest();
                    }
                );

            return response;
        }

        [HttpGet]
        [Authorize]
        [Route("PaymentOption")]
        public IActionResult GetPaymentOption()
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            return Ok(new BaseResponse<IEnumerable<Services.PaymentServices.GetPaymentOptionsService.PaymentOption>>
            {
                Success = true,
                Data = new GetPaymentOptionsService(new GetPaymentOptionsImplementation(_db)).Get(credential)
            });
        }

        [HttpGet]
        [Authorize]
        [Route("PaymentOption/{paymentOptionId}")]
        public IActionResult GetPaymentOption(Guid paymentOptionId)
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            return Ok(new BaseResponse<Services.PaymentServices.GetPaymentOptionsService.PaymentOption>
            {
                Success = true,
                Data = new GetPaymentOptionsService(new GetPaymentOptionsImplementation(_db)).GetById(credential, paymentOptionId)
            });
        }

        [HttpGet]
        [Route("PaymentOptionTypeDescription")]
        public IActionResult GetPaymentOptionTypeDescription()
        {
            return Ok(new BaseResponse<IEnumerable<Services.PaymentServices.GetPaymentOptionTypeDescriptionService.PaymentOptionTypeDescription>>
            {
                Success = true,
                Data = new GetPaymentOptionTypeDescriptionService(new GetPaymentOptionTypeDescriptionImplementation(_db)).Get()
            });
        }
    }
}
