using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoS.Services.PublisherServices;
using MoS.Implementations.PublisherImplementations;
using MoS.Models.CommonUseModels;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Publisher")]
    public class PublisherController : Controller
    {
        private readonly IApplicationDbContext _db;

        public PublisherController(IApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateNewPublisher(Services.PublisherServices.CreatePublisherService.Publisher publisher)
        {
            IActionResult response = null;
            await new CreatePublisherService(new CreatePublisherImplementation(_db)).Create(
                    publisher,
                    () => {
                        response = Ok(new BaseResponse<string>
                        {
                            Success = true
                        });
                    },
                    (exception) => {
                        response = Ok(new ExceptionResponse
                        {
                            ExceptionId = exception
                        });
                    }
                );

            return response;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllPublishers()
        {
            return Ok(new BaseResponse<IEnumerable<Services.PublisherServices.GetPublisherService.Publisher>>
            {
                Success = true,
                Data = new GetPublisherService(new GetPublisherImplementation(_db)).GetAll()
            });
        }
    }
}
