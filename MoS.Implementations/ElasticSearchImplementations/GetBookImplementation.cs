using Microsoft.Extensions.Configuration;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MoS.Implementations.Helpers.SendRequest;
using static MoS.Services.ElasticSearchServices.GetBookService;

namespace MoS.Implementations.ElasticSearchImplementations
{
    public class GetBookImplementation : IGetBook
    {
        private readonly IApplicationDbContext _db;
        public IConfiguration _configuration { get; set; }

        public GetBookImplementation(IApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Book>> Get(string title, int limit)
        {
            var searchRequest = new ElasticSearchBookResquestBody
            {
                Query = new Query
                {
                    Match_bool_prefix = new MatchBoolPrefix
                    {
                        Title = title
                    }
                }
            };

            ElasticSearchBookResponseBody response = null;

            var account = new Account
            {
                Username = _configuration.GetValue<string>("ElasticSearchService:Username"),
                Password = _configuration.GetValue<string>("ElasticSearchService:Password")
            };

            await GetAsync<ElasticSearchBookResquestBody, ElasticSearchBookResponseBody>(
                GetUrl(limit),
                searchRequest,
                account,
                (responseBody) => {
                    response = responseBody;
                },
                () => {

                });
            
            if (response != null)
            {
                var data = response.Hits.hits.Select(hit => new Book
                {
                    Id = hit._source.Id,
                    Title = hit._source.Title
                });

                return data;
            }

            return null;
        }

        private string GetUrl(int limit)
        {
            var url = _configuration.GetValue<string>("ElasticSearchService:Get");

            return url
                .Replace("{size}", limit.ToString());
        }
    }
}
