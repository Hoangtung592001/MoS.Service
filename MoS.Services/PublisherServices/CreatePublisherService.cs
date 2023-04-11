using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.PublisherServices
{
    public class CreatePublisherService
    {
        private readonly ICreatePublisher _repository;
        public CreatePublisherService(ICreatePublisher repository)
        {
            _repository = repository;
        }

        public class Publisher
        {
            public string Name { get; set; }
        }

        public interface ICreatePublisher
        {
            Task Create(Publisher publisher, Action onSuccess, Action<Guid> onFail);
        }

        public async Task Create(Publisher publisher, Action onSuccess, Action<Guid> onFail)
        {
            await _repository.Create(publisher, onSuccess, onFail);
        }
    }
}
