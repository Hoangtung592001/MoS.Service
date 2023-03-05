using Microsoft.Extensions.Configuration;
using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Implementations.Helpers.SendRequest;
using static MoS.Services.ElasticSearchServices.GetBookService;
using static MoS.Services.ElasticSearchServices.SetBookService;

namespace MoS.Implementations.ElasticSearchImplementations
{
    public class SetBookImplementation : ISetBook
    {
        private readonly IApplicationDbContext _db;
        public IConfiguration _configuration { get; set; }

        public SetBookImplementation(IApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task Set(ElasticSearchRequestBody body, Action<ElasticSearchResponseBody> onSuccess, Action onFail)
        {
            var account = new Account
            {
                Username = _configuration.GetValue<string>("ElasticSearchService:Username"),
                Password = _configuration.GetValue<string>("ElasticSearchService:Password")
            };

            await PostAsync<ElasticSearchRequestBody, ElasticSearchResponseBody>(
                _configuration.GetValue<string>("ElasticSearchService:Post"),
                body,
                account,
                (responseBody) => {
                    onSuccess(responseBody);
                },
                () => {
                    onFail();
                });
        }
    }
}
