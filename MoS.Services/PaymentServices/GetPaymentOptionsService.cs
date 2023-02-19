using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.PaymentServices
{
    public class GetPaymentOptionsService
    {
        private readonly IGetPaymentOptions _repository;

        public class PaymentOptionTypeDescription
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class PaymentOption
        {
            public Guid Id { get; set; }
            public string CardNumber { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string NameOnCard { get; set; }
            public int PaymentOptionTypeDescriptionId { get; set; }
            public PaymentOptionTypeDescription PaymentTypeDescription { get; set; }
        }

        public interface IGetPaymentOptions
        {
            IEnumerable<PaymentOption> Get(Credential credential);
        }

        public IEnumerable<PaymentOption> Get(Credential credential)
        {
            return _repository.Get(credential);
        }
    }
}
