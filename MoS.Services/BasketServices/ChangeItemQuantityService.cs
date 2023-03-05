using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.BasketServices
{
    public class ChangeItemQuantityService
    {
        private readonly IChangeItemQuantity _repository;

        public ChangeItemQuantityService(IChangeItemQuantity repository)
        {
            _repository = repository;
        }

        public class ChangeItemQuantityRequest
        {
            public Guid ItemId { get; set; }
            public int Quantity { get; set; }
        }

        public interface IChangeItemQuantity
        {
            Task Set(Credential credential, ChangeItemQuantityRequest request, Action onSuccess, Action onFail);
        }

        public async Task Set(Credential credential, ChangeItemQuantityRequest request, Action onSuccess, Action onFail)
        {
            await _repository.Set(credential, request, onSuccess, onFail);
        }
    }
}
