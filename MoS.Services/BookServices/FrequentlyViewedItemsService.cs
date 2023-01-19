using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoS.Services.BookServices
{
    public class FrequentlyViewedItemsService
    {
        private readonly IFrequentlyViewedItemsService _repository;

        public FrequentlyViewedItemsService(IFrequentlyViewedItemsService repository)
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

        public class FrequentlyViewedItem
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            
            public Author Author { get; set; }
            public BookImage BookImage { get; set; }
        }

        public class FrequentlyViewedItemsRequest
        {
            public int Limit { get; set; }
        }

        public interface IFrequentlyViewedItemsService
        {
            Task<IEnumerable<FrequentlyViewedItem>> Get(FrequentlyViewedItemsRequest request);
        }

        public async Task<IEnumerable<FrequentlyViewedItem>> Get(FrequentlyViewedItemsRequest request)
        {
            return await _repository.Get(request);
        }
    }
}
