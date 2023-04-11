using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.BookServices
{
    public class TrendingItemsService
    {
        private readonly ITrendingItems _repository;

        public TrendingItemsService(ITrendingItems repository)
        {
            _repository = repository;
        }
        public class Author
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class BookImage
        {
            public Guid Id { get; set; }
            public string Url { get; set; }
        }

        public class TrendingItem
        {
            public Guid Id { get; set; }
            public string Title { get; set; }

            public Author Author { get; set; }
            public BookImage BookImage { get; set; }
        }

        public class TrendingItemsRequest
        {
            public int Limit { get; set; }
        }

        public interface ITrendingItems
        {
            Task<IEnumerable<TrendingItem>> Get(int limit);
        }

        public async Task<IEnumerable<TrendingItem>> Get(int limit)
        {
            return await _repository.Get(limit);
        }
    }
}
