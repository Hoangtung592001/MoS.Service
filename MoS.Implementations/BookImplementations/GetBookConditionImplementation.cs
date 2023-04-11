using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.BookServices.GetBookConditionService;

namespace MoS.Implementations.BookImplementations
{
    public class GetBookConditionImplementation : IGetBookCondition
    {
        private readonly IApplicationDbContext _repository;

        public GetBookConditionImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }

        public IEnumerable<BookCondition> GetAll()
        {
            return _repository.BookConditions.Select(bc => new BookCondition
            {
                Id = bc.Id,
                Name = bc.Name
            });
        }
    }
}
