using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.PaymentServices
{
    public class GetPaymentOptionTypeDescriptionService
    {
        private readonly IGetPaymentOptionTypeDescription _repository;

        public class PaymentOptionTypeDescription
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public interface IGetPaymentOptionTypeDescription
        {
            IEnumerable<PaymentOptionTypeDescription> Get(Credential credential);
        }

        public IEnumerable<PaymentOptionTypeDescription> Get(Credential credential)
        {
            return _repository.Get(credential);
        }
    }
}
