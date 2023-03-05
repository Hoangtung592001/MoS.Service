using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.BookServices.SyncBooksToElasticSearchService;
using static MoS.Services.ElasticSearchServices.SetBookService;

namespace MoS.Implementations.BookImplementations
{
    public class SyncBooksToElasticSearchImplementation : ISyncBooksToElasticSearch
    {
        private readonly IApplicationDbContext _db;
        private readonly ISetBook _elasticSetBookService;

        public SyncBooksToElasticSearchImplementation(IApplicationDbContext db, ISetBook elasticSetBookService)
        {
            _db = db;
            _elasticSetBookService = elasticSetBookService;
        }

        public async Task Sync()
        {
            var books = await _db.Books.Where(b => b.IsDeleted == false && b.SyncToElastic == false)
                            .Select(b => new ElasticSearchRequestBody
                            {
                                Id = b.Id,
                                Title = b.Title
                            }).ToListAsync();

            foreach(var book in books)
            {
                await _elasticSetBookService.Set(book, 
                (responseBody) => {
                    var currentBook = _db.Books.Where(b => b.Id.Equals(book.Id)).FirstOrDefault();

                    currentBook.SyncToElastic = true;
                    currentBook.ElasticId = responseBody._Id;
                }, 
                () => { });
            }

            await _db.SaveChangesAsync();
        }
    }
}
