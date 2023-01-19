using System;
using System.Threading.Tasks;

namespace MoS.Services.CommonServices
{
    public class CommonService
    {
        private readonly ICommon _repository;

        public interface ICommon
        {
            Task<bool> CheckAuthorExist(Guid authorId);
            Task<bool> CheckPublisherExist(Guid publisherId);
        }

        public async Task<bool> CheckAuthorExist(Guid authorId)
        {
            return await _repository.CheckAuthorExist(authorId);
        }

        public async Task<bool> CheckPublisherExist(Guid publisherId)
        {
            return await _repository.CheckPublisherExist(publisherId);
        }
    }
}
