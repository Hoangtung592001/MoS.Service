using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;

namespace MoS.Services.PaymentServices
{
    public class SetPaymentOptionsService
    {
        private readonly ISetPaymentOptions _repository;

        public class PaymentOption
        {
            public string CardNumber { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string NameOnCard { get; set; }
            public int PaymentOptionTypeDescriptionId { get; set; }
        }

        public interface ISetPaymentOptions
        {
            IEnumerable<PaymentOption> Set(Credential credential, PaymentOption paymentOption);
        }

        public IEnumerable<PaymentOption> Set(Credential credential, PaymentOption paymentOption)
        {
            return _repository.Set(credential, paymentOption);
        }
    }
}
