using System;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Exception;

namespace MoS.Services.AuthorService
{
    public class CreateAuthorService
    {
        private readonly ICreateAuthorService _repository;
        public CreateAuthorService(ICreateAuthorService repository)
        {
            _repository = repository;
        }

        public class Author
        {
            public string Name { get; set; }
        }

        public interface ICreateAuthorService
        {
            Task Create(Author author, Action onSuccess, Action<Guid> onFail);
        }

        public async Task Create(Author author, Action onSuccess, Action<Guid> onFail)
        {
            await _repository.Create(author, onSuccess, onFail);
        }
    }
}
