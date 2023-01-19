using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.BookServices
{
    public class RecommendedItemsService
    {
        private readonly IRecommendedItems _repository;

        public RecommendedItemsService(IRecommendedItems repository)
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

        public class RecommendedItem
        {
            public Guid Id { get; set; }
            public string Title { get; set; }

            public Author Author { get; set; }
            public BookImage BookImage { get; set; }
        }

        public class RecommendedItemsRequest
        {
            public int Limit { get; set; }
        }

        public interface IRecommendedItems
        {
            Task<IEnumerable<RecommendedItem>> Get(RecommendedItemsRequest request);
        }

        public async Task<IEnumerable<RecommendedItem>> Get(RecommendedItemsRequest request)
        {
            return await _repository.Get(request);
        }
    }
}
