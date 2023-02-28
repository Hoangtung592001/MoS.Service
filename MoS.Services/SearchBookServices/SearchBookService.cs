using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.SearchBookServices
{
    public class SearchBookService
    {
        private readonly ISearchBook _repository;

        public SearchBookService(ISearchBook repository)
        {
            _repository = repository;
        }

        public class SearchBookRequest { 
            public string Title { get; set; }
        }

        public class Book
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }

        public interface ISearchBook
        {
            Task<IEnumerable<Book>> Get(string title);
        }

        public async Task<IEnumerable<Book>> Get(string title)
        {
            return await _repository.Get(title);
        }
    }
}
