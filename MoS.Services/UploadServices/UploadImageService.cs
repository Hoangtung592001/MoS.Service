using Microsoft.AspNetCore.Http;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.UploadServices
{
    public class UploadImageService
    {
        private readonly IUploadImage _repository;

        public UploadImageService(IUploadImage repository)
        {
            _repository = repository;
        }

        public class FileUploadAPI
        {
            public IFormFile? files { get; set; }
        }

        public interface IUploadImage
        {
            void Upload(FileUploadAPI file, Action<string> onSuccess, Action onFail, Credential credential);
        }

        public void Upload(FileUploadAPI file, Action<string> onSuccess, Action onFail, Credential credential)
        {
            _repository.Upload(file, onSuccess, onFail, credential);
        }
    }
}
