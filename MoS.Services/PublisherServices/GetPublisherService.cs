using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.PublisherServices
{
    public class GetPublisherService
    {
        private readonly IGetPublisher _repository;

        public GetPublisherService(IGetPublisher repository)
        {
            _repository = repository;
        }

        public class Publisher
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public interface IGetPublisher
        {
            IEnumerable<Publisher> GetAll();
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
