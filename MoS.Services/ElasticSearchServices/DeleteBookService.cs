using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.ElasticSearchServices
{
    public class DeleteBookService
    {
        private readonly IDeleteBook _repository;

        public DeleteBookService(IDeleteBook repository)
        {
            _repository = repository;
        }

        public class Shards
        {
            public int Total { get; set; }
            public int Successful { get; set; }
            public int Failed { get; set; }
        }

        public class ElasticSearchResponseBody
        {
            public string _Index { get; set; }
            public string _Type { get; set; }
            public string _Id { get; set; }
            public int _Version { get; set; }
            public string _Result { get; set; }
            public Shards _Shards { get; set; }
            public int _Seq_no { get; set; }
            public int _Primary_Term { get; set; }
        }

        public interface IDeleteBook {
            Task Delete(string elasticId, Action<ElasticSearchResponseBody> onSuccess, Action onFail);
        }

        public async Task Delete(string elasticId, Action<ElasticSearchResponseBody> onSuccess, Action onFail)
        {
            await _repository.Delete(elasticId, onSuccess, onFail);
        }
    }
}
