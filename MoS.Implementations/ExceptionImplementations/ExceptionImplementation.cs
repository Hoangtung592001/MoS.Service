using MoS.DatabaseDefinition.Contexts;
using MoS.Services.ExceptionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.ExceptionServices.ExceptionService;
using Exception = MoS.Services.ExceptionServices.ExceptionService.Exception;

namespace MoS.Implementations.ExceptionImplementations
{
    public class ExceptionImplementation : IException
    {
        private readonly IApplicationDbContext _db;

        public ExceptionImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public Exception Get(Guid exceptionId)
        {
            var data =
                    _db.Exceptions.SingleOrDefault(e => e.Id.Equals(exceptionId) && e.IsDeleted == false);

            if (data != null)
            {
                return null;
            }

            return new Exception {
                Id = data.Id,
                Description = data.Description,
                ExceptionMessageType = data.ExceptionMessageType
            };
        }
    }
}
