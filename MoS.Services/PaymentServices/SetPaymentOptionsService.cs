using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoS.Services.PaymentServices
{
    public class SetPaymentOptionsService
    {
        private readonly ISetPaymentOptions _repository;

        public SetPaymentOptionsService(ISetPaymentOptions repository)
        {
            _repository = repository;
        }

        public class PaymentOption
        {
            public string CardNumber { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string NameOnCreditCard { get; set; }
            public int PaymentOptionTypeDescriptionId { get; set; }
        }

        public interface ISetPaymentOptions
        {
            Task Set(Credential credential, PaymentOption paymentOption, Action<Guid> onSuccess, Action onFail);
        }

        public async Task Set(Credential credential, PaymentOption paymentOption, Action<Guid> onSuccess, Action onFail)
        {
            await _repository.Set(credential, paymentOption, onSuccess, onFail);
        }
    }
}
