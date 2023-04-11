using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using static MoS.Implementations.Helpers.SendRequest;
using static MoS.Services.ElasticSearchServices.PutBookService;

namespace MoS.Implementations.ElasticSearchImplementations
{
    public class PutBookImplementation : IPutBook
    {
        public IConfiguration _configuration { get; set; }

        public PutBookImplementation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Put(ElasticSearchRequestBody body, string elasticBookId, Action<ElasticSearchResponseBody> onSuccess, Action onFail)
        {
            var account = new Account
            {
                Username = _configuration.GetValue<string>("ElasticSearchService:Username"),
                Password = _configuration.GetValue<string>("ElasticSearchService:Password")
            };

            await DeleteAsync<ElasticSearchResponseBody>(
                    GetUrl(elasticBookId),
                    account,
                    (responseBody) =>
                    {
                        onSuccess(responseBody);
                    },
                    () =>
                    {
                        onFail();
                    }
                );
        }

        private string GetUrl(string elasticId)
        {
            var url = _configuration.GetValue<string>("ElasticSearchService:Put");

            return url.Replace("{elasticId}", elasticId);
        }
    }
}
