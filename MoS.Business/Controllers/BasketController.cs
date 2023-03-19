using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.BasketImplementations;
using MoS.Implementations.CommonImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.BasketServices;
using MoS.Services.CommonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static MoS.Services.BasketServices.BasketService;
using static MoS.Services.BasketServices.ChangeItemQuantityService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Basket")]
    public class BasketController : Controller
    {
        private readonly IApplicationDbContext _db;
        private readonly CommonService _commonService;
        private readonly IBasket _basketService;

        public BasketController(IApplicationDbContext db, IBasket basketService)
        {
            _db = db;
            _commonService = new CommonService(new CommonImplementation(db));
            _basketService = basketService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            return Ok(
                    new BaseResponse<Basket>
                    {
                        Success = true,
                        Data = await new BasketService(new BasketImplementation(_db, _commonService)).Get(credential)
                    }
                );
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SetBasket(SetBasketRequest request)
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);
            
            var isItemCreated = await new BasketService(new BasketImplementation(_db)).Set(request, credential);
            
            if (!isItemCreated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{BasketItemId}")]
        public async Task<IActionResult> DeleteBasket(Guid BasketItemId)
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            var isDeleted = await new BasketService(new BasketImplementation(_db, _commonService)).Delete(BasketItemId, credential);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("BasketTotal")]
        public async Task<IActionResult> GetBasketTotal()
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            return Ok(
                    new BaseResponse<int>
                    {
                        Success = true,
                        Data = await new BasketService(new BasketImplementation(_db, _commonService)).GetTotal(credential)
                    }
                );
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> ChangeQuantity(ChangeItemQuantityRequest request)
        {
            IActionResult response = null;

            var credential = new CommonService(new CommonImplementation()).GetCredential(User);

            await new ChangeItemQuantityService(
                                new ChangeItemQuantityImplementation(_db, _basketService))
                                .Set(credential, 
                                request, 
                                () => {
                                    response = Ok();
                                }, 
                                () => {
                                    response = BadRequest();
                                });

            return response;
        }

        
    }
}
