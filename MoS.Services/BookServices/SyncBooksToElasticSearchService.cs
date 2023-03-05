using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.BookServices
{
    public class SyncBooksToElasticSearchService
    {
        private readonly ISyncBooksToElasticSearch _repository;

        public SyncBooksToElasticSearchService(ISyncBooksToElasticSearch repository)
        {
            _repository = repository;
        }

        public interface ISyncBooksToElasticSearch
        {
            Task Sync();
        }

        public async Task Sync()
        {
            await _repository.Sync();
        }
    }
}
