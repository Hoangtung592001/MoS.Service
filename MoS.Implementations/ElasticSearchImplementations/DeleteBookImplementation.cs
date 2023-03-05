using Microsoft.Extensions.Configuration;
using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.ElasticSearchServices.DeleteBookService;
using static MoS.Implementations.Helpers.SendRequest;

namespace MoS.Implementations.ElasticSearchImplementations
{
    public class DeleteBookImplementation : IDeleteBook
    {
        private readonly IApplicationDbContext _db;
        public IConfiguration _configuration { get; set; }

        public DeleteBookImplementation(IApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task Delete(string elasticId, Action<ElasticSearchResponseBody> onSuccess, Action onFail)
        {
            var account = new Account
            {
                Username = _configuration.GetValue<string>("ElasticSearchService:Username"),
                Password = _configuration.GetValue<string>("ElasticSearchService:Password")
            };

            await DeleteAsync<ElasticSearchResponseBody>(
                    GetUrl(elasticId),
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
            var url = _configuration.GetValue<string>("ElasticSearchService:Delete");

            return url.Replace("{elasticId}", elasticId);
        }
    }
}
