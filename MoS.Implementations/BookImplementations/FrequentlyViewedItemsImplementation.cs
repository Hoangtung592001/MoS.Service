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
                    await _repository.Books
                        .Where(book => book.IsDeleted == false)
                        .Include(book => book.Author)
                        .Include(book => book.BookImages.Where(image => image.BookImageTypeId == (int)BookImageTypeTDs.Main))
                            .ThenInclude(image => image.BookImageType)
                            .OrderByDescending(book => book.NumberOfViews).Take(request.Limit)
                        .Select(
                            book => new FrequentlyViewedItem
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
                            }).ToListAsync();

            return data;
        }
    }
}
