using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BookImageTypes;
using static MoS.Services.BookServices.FrequentlyViewedItemsService;

namespace MoS.Implementations.BookImplementations
{
    public class FrequentlyViewedItemsImplementation : IFrequentlyViewedItemsService
    {
        private readonly IApplicationDbContext _repository;

        public FrequentlyViewedItemsImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FrequentlyViewedItem>> Get(FrequentlyViewedItemsRequest request)
        {
            var data = 
                    _repository.Books
                        .Where(book => book.IsDeleted == false)
                        .Include(book => book.Author)
                        .Include(book => book.BookImages.Where(image => image.BookImageTypeId == (int)BookImageTypeTDs.Main));
            var numOfViews = await _repository.UserRecentlyViewedItems
                                    .GroupBy(u => u.BookId)
                                    .Select(u => new
                                    {
                                        u.Key,
                                        numOfView = u.Count()
                                    }).OrderByDescending(u => u.numOfView).ToListAsync();

            var orderedData = numOfViews.Join(data,
                n => n.Key,
                d => d.Id,
                (view, book) => new FrequentlyViewedItem
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = new Author
                    {
                        Id = book.Author.Id,
                        Name = book.Author.Name
                    },
                    BookImage = new BookImage
                    {
                        Id = book.BookImages.FirstOrDefault().Id,
                        Url = book.BookImages.FirstOrDefault().Url
                    }
                }).Take(request.Limit);

            return orderedData;
        }
    }
}
