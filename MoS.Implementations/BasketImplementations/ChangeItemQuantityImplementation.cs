using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.BasketServices.BasketService;
using static MoS.Services.BasketServices.ChangeItemQuantityService;
using static MoS.Models.Constants.Enums.Exception;

namespace MoS.Implementations.BasketImplementations
{
    public class ChangeItemQuantityImplementation : IChangeItemQuantity
    {
        private readonly IApplicationDbContext _db;
        private readonly IBasket _basketService;

        public ChangeItemQuantityImplementation(IApplicationDbContext db, IBasket basketService)
        {
            _db = db;
            _basketService = basketService;
        }

        public async Task Set(Credential credential, ChangeItemQuantityRequest request, Action onSuccess, Action<Guid> onFail)
        {
            var item = _db.BasketItems.Where(bi => bi.UserId.Equals(credential.Id)
                                                && bi.IsDeleted == false
                                                && bi.Id.Equals(request.ItemId)).FirstOrDefault();

            var book = _db.Books.Where(b => b.Id.Equals(item.BookId) && b.IsDeleted == false).FirstOrDefault();

            if (item == null)
            {
                onFail(ChangeQuantityExceptionMessages["QUANTITY_NOT_AVAILABLE"]);
                return;
            }

            if (request.Quantity < 0)
            {
                onFail(ChangeQuantityExceptionMessages["INVALID_QUANTITY"]);
                return;
            }

            if (book.Quantity < request.Quantity)
            {
                onFail(ChangeQuantityExceptionMessages["QUANTITY_EXCEED"]);
                return;
            }

            if (request.Quantity > 0)
            {
                item.Quantity = request.Quantity;
            }
            else if (request.Quantity == 0)
            {
                await _basketService.Delete(request.ItemId, credential);
            }

            await _db.SaveChangesAsync();

            onSuccess();
        }
    }
}
