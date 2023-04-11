using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.PublisherServices.GetPublisherService;

namespace MoS.Implementations.PublisherImplementations
{
    public class GetPublisherImplementation : IGetPublisher
    {
        private readonly IApplicationDbContext _db;

        public GetPublisherImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _db.Publishers.Where(p => p.IsDeleted == false).Select(p => new Publisher
            {
                Id = p.Id,
                Name = p.Name
            });
        }
    }
}
