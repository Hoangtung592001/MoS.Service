using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MoS.Services.BookServices.RecentlyViewedItemsService;
using System.Linq;
using static MoS.Models.Constants.Enums.BookImageTypes;
using MoS.Models.CommonUseModels;

namespace MoS.Implementations.BookImplementations
{
    public class RecentlyViewedItemsImplementation : IRecentlyViewedItems
    {
        private readonly IApplicationDbContext _db;

        public RecentlyViewedItemsImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<RecentlyViewedItem>> Get(GetRecentlyViewedItemRequest request)
        {
            var data = await (from viewedItem in _db.UserRecentlyViewedItems
                        join book in _db.Books on viewedItem.BookId equals book.Id
                        join author in _db.Authors on book.AuthorId equals author.Id
                        join bookImage in _db.BookImages on book.Id equals bookImage.BookId where bookImage.BookImageTypeId == (int) BookImageTypeTDs.Main
                        orderby viewedItem.ViewedAt descending
                        select new RecentlyViewedItem
                        {
                            Id = book.Id,
                            Title = book.Title,
                            Author = new Author
                            {
                                Id = author.Id,
                                Name = author.Name
                            },
                            BookImage = new BookImage
                            {
                                Id = bookImage.Id,
                                Url = bookImage.Url
                            }
                        }).ToListAsync()
                        ;

            return data;
        }

        public async Task<bool> Set(SetRecentlyViewedItemRequest request, Credential credential)
        {
            var book = _db.Books.Where(book => book.Id.Equals(request.BookId)).FirstOrDefault();

            if (book == null)
            {
                return false;
            }

            _db.UserRecentlyViewedItems.Add(new DatabaseDefinition.Models.UserRecentlyViewedItem
            {
                BookId = request.BookId,
                UserId = credential.Id,
            });

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
