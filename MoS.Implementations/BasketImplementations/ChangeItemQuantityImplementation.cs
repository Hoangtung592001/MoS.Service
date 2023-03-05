using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.BasketServices.ChangeItemQuantityService;

namespace MoS.Implementations.BasketImplementations
{
    public class ChangeItemQuantityImplementation : IChangeItemQuantity
    {
        private readonly IApplicationDbContext _repository;

        public ChangeItemQuantityImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }

        public Task Set(Credential credential, ChangeItemQuantityRequest request, Action onSuccess, Action onFail)
        {
            throw new NotImplementedException();
        }
    }
}
