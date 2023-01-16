using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using MoS.Services.BookServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BookImageType;

namespace MoS.Implementations.BookImplementations
{
    public class FrequentlyViewedItemsImplementation : FrequentlyViewedItemsService.IFrequentlyViewedItemsService
    {
        private readonly IApplicationDbContext _repository;

        public FrequentlyViewedItemsImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FrequentlyViewedItemsService.FrequentlyViewedItem>> Get()
        {
            var data = (
                    await _repository.Books
                        .Include(book => book.Author)
                        .Include(book => book.BookImages)
                            .ThenInclude(image => image.BookImageType)
                        .Select(
                            book => new FrequentlyViewedItemsService.FrequentlyViewedItem
                            {
                                Id = book.Id,
                                Title = book.Title,
                                Author = new FrequentlyViewedItemsService.Author
                                {
                                    Id = book.Author.Id,
                                    Name = book.Author.Name
                                },
                                BookImage = new FrequentlyViewedItemsService.BookImage
                                {
                                    Id = book.BookImages.FirstOrDefault().Id,
                                    Url = book.BookImages.FirstOrDefault().Url
                                }
                            }).ToListAsync());

            return data;
        }
    }
}
