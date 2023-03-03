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

        public async Task<IEnumerable<Book>> Get(string title)
        {
            var searchRequest = new ElasticSearchBookResquestBody
            {
                Query = new Query
                {
                    Bool = new Bool
                    {
                        Must = new List<dynamic>
                        {
                            new 
                            {
                                match = new
                                {
                                    title = new
                                    {
                                        query = title,
                                        minimum_should_match = "30%"
                                    }
                                }
                            },
                            new
                            {
                                match = new
                                {
                                    isDeleted = new
                                    {
                                        query = 0
                                    }
                                }
                            }
                        }
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
                _configuration.GetValue<string>("ElasticSearchService:Get"),
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
    }
}
