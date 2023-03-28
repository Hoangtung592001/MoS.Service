using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.BookServices.GetAllBooksService;

namespace MoS.Implementations.BookImplementations
{
    public class GetAllBooksImplementation : IGetAllBooksService
    {
        private readonly IApplicationDbContext _repository;

        public GetAllBooksImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }

        public IEnumerable<Book> Get()
        {
            var data = 
                    _repository.Books
                        .Include(book => book.Author)
                        .Include(book => book.Author)
                        .Include(book => book.BookImages)
                        .Include(book => book.BookCondition)
                        .Where(book => book.IsDeleted == false)
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
                                Description = book.Description,
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
                ;

            return data;
        }
    }
}
