using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BasketItemTypeDescription;
using static MoS.Services.CommonServices.CommonService;
namespace MoS.Implementations.BookImplementations
{
    public class DeleteBookImplementation : Services.BookServices.DeleteBookService.IDeleteBook
    {
        private readonly IApplicationDbContext _db;
        private readonly Services.ElasticSearchServices.DeleteBookService.IDeleteBook _deleteBookService;
        private readonly ICommon _commonService;

        public DeleteBookImplementation(IApplicationDbContext db, Services.ElasticSearchServices.DeleteBookService.IDeleteBook deleteBookService, ICommon commonService)
        {
            _db = db;
            _deleteBookService = deleteBookService;
            _commonService = commonService;
        }


        public DeleteBookImplementation(IApplicationDbContext db, Services.ElasticSearchServices.DeleteBookService.IDeleteBook deleteBookService)
        {
            _db = db;
            _deleteBookService = deleteBookService;
        }

        public async Task<bool> Delete(Guid BookId, Credential credential)
        {
            var book = _db.Books.SingleOrDefault(b => b.Id.Equals(BookId) && b.IsDeleted == false);

            var basketItems = _db.BasketItems.Where(bi => bi.BookId.Equals(BookId) && bi.IsDeleted == false && bi.BasketItemTypeDescriptionId == (int) BasketItemTypeDescriptionIDs.InBasket);

            if (book != null)
            {
                book.IsDeleted = true;
                book.DeletedAt = DateTime.Now;
                book.DeletedBy = credential.Id;

                foreach (var basketItem in basketItems)
                {
                    _commonService.DeleteItem(basketItem, credential.Id);
                }

                if (book.SyncToElastic == true)
                {
                    try
                    {
                        await _deleteBookService.Delete(book.ElasticId, (responseBody) => {
                            book.SyncToElastic = false;
                            book.ElasticId = null;
                        }, () => { });
                    }
                    catch { }
                }

                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
