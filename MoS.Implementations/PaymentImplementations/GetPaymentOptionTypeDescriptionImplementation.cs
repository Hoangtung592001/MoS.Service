using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.PaymentServices.GetPaymentOptionTypeDescriptionService;

namespace MoS.Implementations.PaymentImplementations
{
    public class GetPaymentOptionTypeDescriptionImplementation : IGetPaymentOptionTypeDescription
    {
        private readonly IApplicationDbContext _db;

        public GetPaymentOptionTypeDescriptionImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<PaymentOptionTypeDescription> Get()
        {
            var data = _db.PaymentOptionTypeDescriptions
                    .Where(potd => potd.IsDeleted == false)
                    .Select(potd => new PaymentOptionTypeDescription
                    {
                        Id = potd.Id,
                        Description = potd.Description,
                        Name = potd.Name
                    })
                    .AsEnumerable();

            return data;
        }
    }
}
