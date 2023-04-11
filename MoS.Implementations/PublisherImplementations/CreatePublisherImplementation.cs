using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.PublisherServices.CreatePublisherService;

namespace MoS.Implementations.PublisherImplementations
{
    public class CreatePublisherImplementation : ICreatePublisher
    {
        private readonly IApplicationDbContext _repository;

        public CreatePublisherImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }

        public async Task Create(Publisher publisher, Action onSuccess, Action<Guid> onFail)
        {
            var newAuthor = new DatabaseDefinition.Models.Publisher
            {
                Name = publisher.Name
            };

            _repository.Publishers.Add(newAuthor);

            await _repository.SaveChangesAsync();

            onSuccess();
        }
    }
}
