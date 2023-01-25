using MoS.Models.CommonUseModels;
using System;
using System.Threading.Tasks;

namespace MoS.Services.BookServices
{
    public class DeleteBookService
    {
        private readonly IDeleteBook _repository;

        public DeleteBookService(IDeleteBook repository)
        {
            _repository = repository;
        }

        public interface IDeleteBook
        {
            Task<bool> Delete(Guid BookId, Credential credential);
        }

        public async Task<bool> Delete(Guid BookId, Credential credential)
        {
            return await _repository.Delete(BookId, credential);
        }
    }
}
