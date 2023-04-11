using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.AuthorServices.GetAuthorService;

namespace MoS.Implementations.AuthorImplementations
{
    public class GetAuthorImplementation : IGetAuthor
    {
        private readonly IApplicationDbContext _db;

        public GetAuthorImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Author> GetAll()
        {
            return _db.Authors.Where(a => a.IsDeleted == false).Select(a => new Author{ 
                Id = a.Id,
                Name = a.Name
            });
        }
    }
}
