using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BookImageTypes;
using static MoS.Services.BookServices.RecommendedItemsService;

namespace MoS.Implementations.BookImplementations
{
    public class RecommendedItemsImplementation : IRecommendedItems
    {
        private readonly IApplicationDbContext _repository;

        public RecommendedItemsImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<RecommendedItem>> Get(RecommendedItemsRequest request)
        {
            var data =
                    await _repository.Books
                        .Include(book => book.Author)
                        .Include(book => book.BookImages.Where(image => image.BookImageTypeId == (int)BookImageTypeTDs.Main))
                            .ThenInclude(image => image.BookImageType)
                        .Where(book => book.IsDeleted == false)
                        .Select(
                            book => new RecommendedItem
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
                            })
                        .ToListAsync();

            return data;
        }
    }
}
