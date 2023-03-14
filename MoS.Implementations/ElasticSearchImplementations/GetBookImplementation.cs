using Microsoft.EntityFrameworkCore;
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
using static MoS.Models.Constants.Enums.BookImageTypes;
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

        public async Task<IEnumerable<WholeBook>> GetWhole(string title, int limit)
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
                }).ToList();

                var bookIDs = data.Select(b => b.Id);

                var searchedBooks = _db.Books
                                    .Include(book => book.Author)
                                    .Include(book => book.BookImages.Where(image => image.BookImageTypeId == (int)BookImageTypeTDs.Main))
                                        .ThenInclude(image => image.BookImageType)
                                    .Where(b => bookIDs.Contains(b.Id)).Select(b => new WholeBook
                                    {
                                        Id = b.Id,
                                        Title = b.Title,
                                        Url = b.BookImages.FirstOrDefault().Url,
                                        Author = b.Author.Name
                                    });

                return searchedBooks;
            }

            return null;
        }
    }
}
