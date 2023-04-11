using Microsoft.AspNetCore.Mvc;
using MoS.Models.CommonUseModels;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoS.Services.CommonServices
{
    public class CommonService
    {
        private readonly ICommon _repository;

        public CommonService(ICommon repository)
        {
            _repository = repository;
        }

        public interface ICommon
        {
            Task<bool> CheckAuthorExist(Guid authorId);
            Task<bool> CheckPublisherExist(Guid publisherId);
            Credential GetCredential(ClaimsPrincipal User);
            double SellOfPrice(double originalPrice, double sellOffRate);
            void DeleteItem(dynamic item, Guid deletedBy);
            string GetFileName(string fileName, string userId);
        }

        public async Task<bool> CheckAuthorExist(Guid authorId)
        {
            return await _repository.CheckAuthorExist(authorId);
        }

        public async Task<bool> CheckPublisherExist(Guid publisherId)
        {
            return await _repository.CheckPublisherExist(publisherId);
        }

        public Credential GetCredential(ClaimsPrincipal User)
        {
            return _repository.GetCredential(User);
        }

        public double SellOfPrice(double originalPrice, double sellOffRate)
        {
            return _repository.SellOfPrice(originalPrice, sellOffRate);
        }

        public void DeleteItem(dynamic item, Guid deletedBy)
        {
            _repository.DeleteItem(item, deletedBy);
        }

        public string GetFileName(string fileName, string userId)
        {
            return _repository.GetFileName(fileName, userId);
        }
    }
}
