using MoS.DatabaseDefinition.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MoS.Services.BasketServices.BasketService;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MoS.Models.CommonUseModels;
using System;

namespace MoS.Implementations.BasketImplementations
{
    public class BasketImplementation : IBasket
    {
        private readonly IApplicationDbContext _db;

        public BasketImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<BasketItem>> Get(Credential credential)
        {
            var data = (
                    _db.BasketItems
                        .Include(item => item.Book)
                            .ThenInclude(book => book.Author)
                        .Include(item => item.Book)
                            .ThenInclude(book => book.Publisher)
                        .Where(item => item.UserId == credential.Id)
                        .Select(
                            item => new BasketItem
                            {
                                Id = item.Id,
                                BookId = item.BookId,
                                UserId = item.UserId,
                                Book = new Book
                                {
                                    Id = item.Book.Id,
                                    Title = item.Book.Title,
                                    AuthorId = item.Book.AuthorId,
                                    PublisherId = item.Book.PublisherId,
                                    BookConditionId = item.Book.BookConditionId,
                                    Quantity = item.Quantity,
                                    Price = item.Book.Price,
                                    SellOffRate = item.Book.SellOffRate,
                                    Edition = item.Book.Edition,
                                    Author = new Author
                                    {
                                        Id = item.Book.Author.Id,
                                        Name = item.Book.Author.Name
                                    },
                                    Publisher = new Publisher
                                    {
                                        Id = item.Book.Publisher.Id,
                                        Name = item.Book.Publisher.Name
                                    }
                                }
                            }
                        )
                );

            return data;
        }

        public async Task<bool> Set(SetBasketRequest request, Credential credential)
        {
            var book = _db.Books.Where(book => book.Id.Equals(request.BookId)).FirstOrDefault();

            if (book == null)
            {
                return false;
            }

            _db.BasketItems.Add(
                    new DatabaseDefinition.Models.BasketItem
                    {
                        Id = Guid.NewGuid(),
                        BookId = request.BookId,
                        UserId = credential.Id,
                        Quantity = request.Quantity,
                    }
                );

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
