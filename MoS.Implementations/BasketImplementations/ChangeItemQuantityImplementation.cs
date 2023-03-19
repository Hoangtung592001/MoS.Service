using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.BasketServices.BasketService;
using static MoS.Services.BasketServices.ChangeItemQuantityService;

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

        public async Task Set(Credential credential, ChangeItemQuantityRequest request, Action onSuccess, Action onFail)
        {
            var item = _db.BasketItems.Where(bi => bi.UserId.Equals(credential.Id)
                                                && bi.IsDeleted == false
                                                && bi.Id.Equals(request.ItemId)).FirstOrDefault();

            if (item == null)
            {
                onFail();
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
