using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BasketItemTypeDescription;
using static MoS.Models.Constants.Enums.BookImageTypes;
using static MoS.Services.BookServices.TrendingItemsService;

namespace MoS.Implementations.BookImplementations
{
    public class TrendingItemsImplementation : ITrendingItems
    {
        private readonly IApplicationDbContext _db;

        public TrendingItemsImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TrendingItem>> Get(int limit)
        {
            var orderDetails = _db.BasketItems
                                    .Where(bi => bi.BasketItemTypeDescriptionId == (int)BasketItemTypeDescriptionIDs.Ordered)
                                    .GroupBy(bi => bi.BookId)
                                    .Select(
                                        bi => new
                                        {
                                            bi.Key,
                                            quantity = bi.Sum(b => b.Quantity)
                                        }
                                    ).OrderByDescending(bi => bi.quantity);

            var data =
                    _db.Books
                        .Where(book => book.IsDeleted == false)
                        .Include(book => book.Author)
                        .Include(book => book.BookImages.Where(image => image.BookImageTypeId == (int)BookImageTypeTDs.Main));


            var orderedData = await orderDetails.Join(data,
                n => n.Key,
                d => d.Id,
                (view, book) => new TrendingItem
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
                }).Take(limit).ToListAsync();

            return orderedData;
        }
    }
}
