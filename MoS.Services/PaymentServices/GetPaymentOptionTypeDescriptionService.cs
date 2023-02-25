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

        public GetPaymentOptionTypeDescriptionService(IGetPaymentOptionTypeDescription repository)
        {
            _repository = repository;
        }

        public class PaymentOptionTypeDescription
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public interface IGetPaymentOptionTypeDescription
        {
            IEnumerable<PaymentOptionTypeDescription> Get();
        }

        public IEnumerable<PaymentOptionTypeDescription> Get()
        {
            return _repository.Get();
        }
    }
}
