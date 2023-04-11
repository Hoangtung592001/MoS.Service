using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MoS.Services.BookServices.RecentlyViewedItemsService;
using System.Linq;
using static MoS.Models.Constants.Enums.BookImageTypes;
using MoS.Models.CommonUseModels;
using System;

namespace MoS.Implementations.BookImplementations
{
    public class RecentlyViewedItemsImplementation : IRecentlyViewedItems
    {
        private readonly IApplicationDbContext _db;

        public RecentlyViewedItemsImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<RecentlyViewedItem>> Get(GetRecentlyViewedItemRequest request, Credential credential)
        {
            var items = _db.UserRecentlyViewedItems
                .Where(c => c.UserId.Equals(credential.Id))
                .OrderByDescending(x => x.ViewedAt)
                .Select(c => c.BookId);

            var distinctItems = new HashSet<Guid>(items).ToList();

            var books =
                    await _db.Books
                        .Where(book => book.IsDeleted == false && distinctItems.Contains(book.Id))
                        .Include(book => book.Author)
                        .Include(book => book.BookImages.Where(image => image.BookImageTypeId == (int)BookImageTypeTDs.Main)).ToListAsync();
            
            var data = distinctItems.Join(books,
                        i => i,
                        b => b.Id,
                        (item, book) => new RecentlyViewedItem
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
                        }
                    ).Take(request.Limit);

            return data;
        }

        public async Task<bool> Set(SetRecentlyViewedItemRequest request, Credential credential)
        {
            var book = _db.Books.Where(book => book.Id.Equals(request.BookId) && book.IsDeleted == false).FirstOrDefault();

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
