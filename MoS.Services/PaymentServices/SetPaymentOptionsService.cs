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
            public string NameOnCard { get; set; }
            public int PaymentOptionTypeDescriptionId { get; set; }
        }

        public interface ISetPaymentOptions
        {
            Task<bool> Set(Credential credential, PaymentOption paymentOption);
        }

        public async Task<bool> Set(Credential credential, PaymentOption paymentOption)
        {
            return await _repository.Set(credential, paymentOption);
        }
    }
}
