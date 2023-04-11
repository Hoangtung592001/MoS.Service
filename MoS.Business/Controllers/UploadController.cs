using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.CommonImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.CommonServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using static MoS.Services.UploadServices.UploadImageService;

namespace MoS.Business.Controllers
{
    [ApiController]
    [Route("Upload")]
    public class UploadController : Controller
    {
        private readonly IApplicationDbContext _db;
        private readonly IUploadImage _uploadImageService;

        public UploadController(IApplicationDbContext db, IUploadImage uploadImageService)
        {
            _db = db;
            _uploadImageService = uploadImageService;
        }
        
        [HttpPost]
        [Authorize]
        [Route("UploadFile")]
        public IActionResult UploadFile([FromForm] FileUploadAPI file)
        {
            var credential = new CommonService(new CommonImplementation()).GetCredential(User);
            IActionResult response = null;

            _uploadImageService.Upload(
                file,
                (fileName) =>
                {
                    response = Ok(new BaseResponse<string>
                    {
                        Success = true,
                        Data = fileName
                    });
                },
                () =>
                {
                    response = BadRequest();
                }, credential);

            return response;
        }
    }
}
