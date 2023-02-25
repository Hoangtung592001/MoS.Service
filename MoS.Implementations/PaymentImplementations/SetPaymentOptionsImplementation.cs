using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.PaymentServices.SetPaymentOptionsService;

namespace MoS.Implementations.PaymentImplementations
{
    public class SetPaymentOptionsImplementation : ISetPaymentOptions
    {
        private readonly IApplicationDbContext _db;

        public SetPaymentOptionsImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Set(Credential credential, PaymentOption paymentOption, Action<Guid> onSuccess, Action onFail)
        {
            var paymentOptionTypeDescription = _db
                                            .PaymentOptionTypeDescriptions
                                            .Where(potd => potd.Id == paymentOption.PaymentOptionTypeDescriptionId && potd.IsDeleted == false)
                                            .FirstOrDefault();

            if (paymentOptionTypeDescription == null)
            {
                onFail();
            }

            var paymentOptionId = Guid.NewGuid();

            _db.PaymentOptions.Add(new DatabaseDefinition.Models.PaymentOption
            {
                Id = paymentOptionId,
                CardNumber = paymentOption.CardNumber,
                ExpiryDate = paymentOption.ExpiryDate,
                NameOnCard = paymentOption.NameOnCreditCard,
                PaymentOptionTypeDescriptionId = paymentOption.PaymentOptionTypeDescriptionId,
                UserId = credential.Id
            });

            await _db.SaveChangesAsync();

            onSuccess(paymentOptionId);
        }
    }
}
