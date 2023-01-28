using MoS.DatabaseDefinition.Contexts;
using MoS.DatabaseDefinition.Models;
using MoS.Services.AuthorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Exception;
using static MoS.Services.AuthorService.CreateAuthorService;

namespace MoS.Implementations.AuthorImplementations
{
    public class CreateAuthorImplementation : ICreateAuthorService
    {
        private readonly IApplicationDbContext _repository;

        public CreateAuthorImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }

        public async Task Create(CreateAuthorService.Author author, Action onSuccess, Action<Guid> onFail)
        {
            var newAuthor = new DatabaseDefinition.Models.Author
            {
                Name = author.Name
            };

            _repository.Authors.Add(newAuthor);

            await _repository.SaveChangesAsync();

            onSuccess();
        }
    }
}
