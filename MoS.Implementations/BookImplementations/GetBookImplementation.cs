using MoS.DatabaseDefinition.Contexts;
using System;
using System.Threading.Tasks;
using static MoS.Services.BookServices.GetBookService;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MoS.Implementations.BookImplementations
{
    public class GetBookImplementation : IGetBook
    {
        private readonly IApplicationDbContext _db;

        public GetBookImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Book> Get(Guid BookId)
        {
            var data = (await (
                    _db.Books
                        .Include(book => book.Author)
                        .Include(book => book.Author)
                        .Include(book => book.BookImages)
                        .Include(book => book.BookCondition)
                        .Where(book => book.Id.Equals(BookId))
                        .Select(
                            book => new Book
                            {
                                Id = book.Id,
                                Title = book.Title,
                                AuthorId = book.AuthorId,
                                PublisherId = book.PublisherId,
                                PublishedAt = book.PublishedAt,
                                StartedToSellAt = book.StartedToSellAt,
                                CreatedAt = book.CreatedAt,
                                BookConditionId = book.BookConditionId,
                                Quantity = book.Quantity,
                                Price = book.Price,
                                SellOffRate = book.SellOffRate,
                                Edition = book.Edition,
                                NumberOfViews = book.NumberOfViews,
                                BookDetails = book.BookDetails,
                                Author = new Author
                                {
                                    Id = book.Author.Id,
                                    Name = book.Author.Name
                                },
                                Publisher = new Publisher
                                {
                                    Id = book.Publisher.Id,
                                    Name = book.Publisher.Name
                                },
                                BookCondition = new BookCondition
                                {
                                    Id = book.BookCondition.Id,
                                    Name = book.BookCondition.Name
                                },
                                BookImages = book.BookImages.Select(image => new BookImage
                                {
                                    Id = image.Id,
                                    Url = image.Url,
                                    BookImageTypeId = image.BookImageTypeId
                                }).ToList()
                            }
                        )
                ).ToListAsync()).FirstOrDefault();

            return data;
        }
    }
}
