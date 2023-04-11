using Microsoft.AspNetCore.Hosting;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.CommonServices.CommonService;
using static MoS.Services.UploadServices.UploadImageService;

namespace MoS.Implementations.UploadImplementations
{
    public class UploadImageImplementation : IUploadImage
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ICommon _commonService;

        public UploadImageImplementation(IWebHostEnvironment environment, ICommon commonService)
        {
            _environment = environment;
            _commonService = commonService;
        }

        public void Upload(FileUploadAPI file, Action<string> onSuccess, Action onFail, Credential credential)
        {
            try
            {
                if (file.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    var fileLink = _commonService.GetFileName(file.files.FileName, credential.Id.ToString());

                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + fileLink))
                    {
                        file.files.CopyTo(filestream);
                        filestream.Flush();
                    }

                    onSuccess(fileLink);
                }
            }
            catch 
            {
                onFail();
            }
        }
    }
}
