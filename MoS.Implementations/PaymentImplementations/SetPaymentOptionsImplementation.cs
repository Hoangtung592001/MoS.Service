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

        public async Task<bool> Set(Credential credential, PaymentOption paymentOption)
        {
            var paymentOptionTypeDescription = _db
                                            .PaymentOptionTypeDescriptions
                                            .Where(potd => potd.Id == paymentOption.PaymentOptionTypeDescriptionId && potd.IsDeleted == false)
                                            .FirstOrDefault();

            if (paymentOptionTypeDescription == null)
            {
                return false;
            }

            _db.PaymentOptions.Add(new DatabaseDefinition.Models.PaymentOption
            {
                Id = Guid.NewGuid(),
                CardNumber = paymentOption.CardNumber,
                ExpiryDate = paymentOption.ExpiryDate,
                NameOnCard = paymentOption.NameOnCard,
                PaymentOptionTypeDescriptionId = paymentOption.PaymentOptionTypeDescriptionId,
                UserId = credential.Id
            });

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
