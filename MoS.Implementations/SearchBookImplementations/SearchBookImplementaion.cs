using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.SearchBookServices.SearchBookService;

namespace MoS.Implementations.SearchBookImplementations
{
    public class SearchBookImplementaion : ISearchBook
    {
        private readonly IApplicationDbContext _db;

        public SearchBookImplementaion(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Book>> Get(string title)
        {
            throw new NotImplementedException();
        }
    }
}
