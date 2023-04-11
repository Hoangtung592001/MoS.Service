using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.BookServices
{
    public class RecentlyViewedItemsService
    {
        public IRecentlyViewedItems _repository;

        public RecentlyViewedItemsService(IRecentlyViewedItems repository)
        {
            _repository = repository;
        }

        public class GetRecentlyViewedItemRequest
        {
            public int Limit { get; set; }
        }

        public class SetRecentlyViewedItemRequest
        {
            public Guid BookId { get; set; }
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

        public class RecentlyViewedItem
        {
            public Guid Id { get; set; }
            public string Title { get; set; }

            public Author Author { get; set; }
            public BookImage BookImage { get; set; }
        }

        public interface IRecentlyViewedItems
        {
            Task<IEnumerable<RecentlyViewedItem>> Get(GetRecentlyViewedItemRequest request, Credential credential);
            Task<bool> Set(SetRecentlyViewedItemRequest request, Credential credential);
        }

        public async Task<IEnumerable<RecentlyViewedItem>> Get(GetRecentlyViewedItemRequest request, Credential credential)
        {
            return await _repository.Get(request, credential);
        }

        public async Task<bool> Set(SetRecentlyViewedItemRequest request, Credential credential)
        {
            return await _repository.Set(request, credential);
        }
    }
}
