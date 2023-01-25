using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Services.BookServices.DeleteBookService;

namespace MoS.Implementations.BookImplementations
{
    public class DeleteBookImplementation : IDeleteBook
    {
        private readonly IApplicationDbContext _db;

        public DeleteBookImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Delete(Guid BookId, Credential credential)
        {
            var book = _db.Books.SingleOrDefault(b => b.Id.Equals(BookId));

            if (book != null)
            {
                book.IsDeleted = true;
                book.DeletedAt = DateTime.Now;
                book.DeletedBy = credential.Id;

                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
