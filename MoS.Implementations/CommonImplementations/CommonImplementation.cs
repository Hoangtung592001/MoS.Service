using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.CommonServices.CommonService;

namespace MoS.Implementations.CommonImplementations
{
    public class CommonImplementation : ICommon
    {
        private readonly IApplicationDbContext _repository;

        public CommonImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }
        public async Task<bool> CheckAuthorExist(Guid authorId)
        {
            var author = (await _repository.Authors.Where(a => a.Id.Equals(authorId)).ToListAsync()).FirstOrDefault();

            if (author != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CheckPublisherExist(Guid publisherId)
        {
            var publisher = (await _repository.Publishers.Where(a => a.Id.Equals(publisherId)).ToListAsync()).FirstOrDefault();

            if (publisher != null)
            {
                return true;
            }

            return false;
        }
    }
}
