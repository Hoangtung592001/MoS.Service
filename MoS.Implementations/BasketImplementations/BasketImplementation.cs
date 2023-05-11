using MoS.DatabaseDefinition.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MoS.Services.BasketServices.BasketService;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MoS.Models.CommonUseModels;
using System;
using MoS.Services.CommonServices;
using static MoS.Models.Constants.Enums.BookImageTypes;
using static MoS.Models.Constants.Enums.BasketItemTypeDescription;
using static MoS.Models.Constants.Enums.Exception;

namespace MoS.Implementations.BasketImplementations
{
    public class BasketImplementation : IBasket
    {
        private readonly IApplicationDbContext _db;
        private readonly CommonService _commonService;

        public BasketImplementation(IApplicationDbContext db, CommonService commonService)
        {
            _db = db;
            _commonService = commonService;
        }

        public BasketImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Delete(Guid BasketItemId, Credential credential)
        {
            var basketItem = _db.BasketItems.SingleOrDefault(item => item.Id.Equals(BasketItemId) && item.UserId.Equals(credential.Id));

            if (basketItem != null)
            {
                _commonService.DeleteItem(basketItem, credential.Id);

                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Basket> Get(Credential credential)
        {
            var basketItems = (
                    _db.BasketItems
                        .Include(item => item.Book)
                            .ThenInclude(book => book.Author)
                        .Include(item => item.Book)
                            .ThenInclude(book => book.Publisher)
                        .Include(item => item.Book)
                            .ThenInclude(book => book.BookImages)
                        .Where(item => item.UserId == credential.Id && 
                                item.Book.IsDeleted == false && item.IsDeleted == false && 
                                item.BasketItemTypeDescriptionId == (int)BasketItemTypeDescriptionIDs.InBasket)
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
                                    },
                                    BookImage = new BookImage
                                    {
                                        Id = item.Book.BookImages.FirstOrDefault(image => image.BookImageTypeId == (int) BookImageTypeTDs.Main).Id,
                                        Url = item.Book.BookImages.FirstOrDefault(image => image.BookImageTypeId == (int) BookImageTypeTDs.Main).Url,
                                    }
                                },
                                IsQuantityValid = item.Book.Quantity >= item.Quantity
                            }
                        )
                ).ToList();

            decimal orderTotal = 0;

            foreach(BasketItem item in basketItems)
            {
                orderTotal += item.Book.Quantity * _commonService.SellOfPrice(item.Book.Price, item.Book.SellOffRate);
            }

            var data = new Basket
            {
                BasketItems = basketItems,
                OrderTotal = orderTotal
            };

            return data;
        }

        public async Task Set(SetBasketRequest request, Credential credential, Action onSuccess, Action<Guid> onFail)
        {
            var book = _db.Books.Where(book => book.Id.Equals(request.BookId) && book.IsDeleted == false).FirstOrDefault();

            if (book == null)
            {
                onFail(Guid.Empty);
            }

            if (book.Quantity < request.Quantity)
            {
                onFail(ChangeQuantityExceptionMessages["QUANTITY_EXCEED"]);
                return;
            }

            var basketItem = await _db.BasketItems
                                .Include(item => item.Book)
                                .Where(item => item.UserId.Equals(credential.Id)
                                        && item.BookId.Equals(request.BookId)
                                        && item.IsDeleted == false
                                        && item.BasketItemTypeDescriptionId == (int) BasketItemTypeDescriptionIDs.InBasket
                                        ).FirstOrDefaultAsync();

            if (basketItem != null)
            {
                basketItem.Quantity++;
            }
            else
            {
                _db.BasketItems.Add(
                        new DatabaseDefinition.Models.BasketItem
                        {
                            Id = Guid.NewGuid(),
                            BookId = request.BookId,
                            UserId = credential.Id,
                            Quantity = request.Quantity,
                        }
                    );
            }


            await _db.SaveChangesAsync();

            onSuccess();
        }

        public async Task<int> GetTotal(Credential credential)
        {
            var total = await _db.BasketItems
                .Include(item => item.Book)
                .Where(item => item.UserId == credential.Id && 
                        item.IsDeleted == false && 
                        item.Book.IsDeleted == false &&
                        item.BasketItemTypeDescriptionId == (int)BasketItemTypeDescriptionIDs.InBasket)
                .CountAsync();

            return total;
        }
    }
}
