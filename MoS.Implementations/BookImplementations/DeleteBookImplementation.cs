using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using MoS.Services.CommonServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Services.BasketServices.BasketService;
using static MoS.Services.BookServices.DeleteBookService;
using static MoS.Services.CommonServices.CommonService;
using static MoS.Services.ElasticSearchServices.DeleteBookService;
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

            var basketItems = _db.BasketItems.Where(bi => bi.BookId.Equals(BookId));

            if (book != null)
            {
                book.IsDeleted = true;
                book.DeletedAt = DateTime.Now;
                book.DeletedBy = credential.Id;

                foreach(var basketItem in basketItems)
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
