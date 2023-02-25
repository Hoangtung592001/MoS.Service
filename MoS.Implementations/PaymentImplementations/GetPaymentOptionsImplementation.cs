using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.PaymentServices.GetPaymentOptionsService;

namespace MoS.Implementations.PaymentImplementations
{
    public class GetPaymentOptionsImplementation : IGetPaymentOptions
    {
        private readonly IApplicationDbContext _db;

        public GetPaymentOptionsImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<PaymentOption> Get(Credential credential)
        {
            var data = _db
                    .PaymentOptions
                    .Include(po => po.PaymentTypeDescription)
                    .Where(po => po.UserId == credential.Id && po.IsDeleted == false)
                    .Select(po => new PaymentOption
                    {
                        Id = po.Id,
                        CardNumber = po.CardNumber,
                        ExpiryDate = po.ExpiryDate,
                        NameOnCard = po.NameOnCard,
                        PaymentOptionTypeDescriptionId = po.PaymentOptionTypeDescriptionId,
                        PaymentOptionTypeDescription = new PaymentOptionTypeDescription
                        {
                            Id = po.PaymentTypeDescription.Id,
                            Description = po.PaymentTypeDescription.Description,
                            Name = po.PaymentTypeDescription.Name
                        }
                    }).AsEnumerable();

            return data;
        }

        public PaymentOption GetById(Credential credential, Guid paymentOptionId)
        {
            var data = _db
                    .PaymentOptions
                    .Include(po => po.PaymentTypeDescription)
                    .Where(po => po.UserId.Equals(credential.Id) && po.IsDeleted == false && po.Id.Equals(paymentOptionId))
                    .Select(po => new PaymentOption
                    {
                        Id = po.Id,
                        CardNumber = po.CardNumber,
                        ExpiryDate = po.ExpiryDate,
                        NameOnCard = po.NameOnCard,
                        PaymentOptionTypeDescriptionId = po.PaymentOptionTypeDescriptionId,
                        PaymentOptionTypeDescription = new PaymentOptionTypeDescription
                        {
                            Id = po.PaymentTypeDescription.Id,
                            Description = po.PaymentTypeDescription.Description,
                            Name = po.PaymentTypeDescription.Name
                        }
                    }).FirstOrDefault();

            return data;
        }
    }
}
