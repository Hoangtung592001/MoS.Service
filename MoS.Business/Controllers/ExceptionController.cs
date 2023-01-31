using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.ExceptionImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.ExceptionServices;
using System;
using Exception = MoS.Services.ExceptionServices.ExceptionService.Exception;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Exception")]
    public class ExceptionController : Controller
    {
        private readonly IApplicationDbContext _db;

        public ExceptionController(IApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("{ExceptionId}")]
        public IActionResult Index(Guid exceptionId)
        {
            var exception = new ExceptionService(new ExceptionImplementation(_db)).Get(exceptionId);

            return Ok(new BaseResponse<Exception>{
                Success = true,
                Data = exception
            });
        }
    }
}
