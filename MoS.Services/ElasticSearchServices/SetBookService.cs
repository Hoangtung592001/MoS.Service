using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.ElasticSearchServices
{
    public class SetBookService
    {
        private readonly ISetBook _repository;

        public SetBookService(ISetBook repository)
        {
            _repository = repository;
        }

        public class ElasticSearchRequestBody
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public bool IsDeleted { get; set; }
        }

        public interface ISetBook
        {
            Task Set(ElasticSearchRequestBody body, Action onSuccess, Action onFail);
        }

        public async Task Set(ElasticSearchRequestBody body, Action onSuccess, Action onFail)
        {
            await _repository.Set(body, onSuccess, onFail);
        }
    }
}
