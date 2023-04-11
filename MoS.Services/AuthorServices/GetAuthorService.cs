using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.AuthorServices
{
    public class GetAuthorService
    {
        private readonly IGetAuthor _repository;
        public GetAuthorService(IGetAuthor repository)
        {
            _repository = repository;
        }

        public class Author
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public interface IGetAuthor
        {
            IEnumerable<Author> GetAll();
        }

        public IEnumerable<Author> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
