using MoS.DatabaseDefinition.Contexts;
using MoS.Services.BookServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.BookServices.CreateBookService;
using static MoS.Services.BookServices.EditBookService;

namespace MoS.Implementations.BookImplementations
{
    public class EditBookImplementation : IEditBook
    {
        private readonly IApplicationDbContext _repository;

        public EditBookImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }

        public async Task Edit(EditBookRequest request, Action onSuccess, Action onFail)
        {
            var book = _repository.Books.Where(b => b.Id.Equals(request.Id)).FirstOrDefault();

            if (book == null)
            {
                onFail();
                return;
            }

            book.Title = request.Title;
            book.AuthorId = request.AuthorId;
            book.PublisherId = request.PulisherId;
            book.PublishedAt = request.PublishedAt;
            book.BookConditionId = request.BookConditionId;
            book.Quantity = request.Quantity;
            book.Price = request.Price;
            book.Edition = request.Edition;
            book.Description = request.Description;
            book.BookDetails = request.BookDetails;

            var mainImage = _repository.BookImages.Where(bi => bi.Id.Equals(request.Image.Id)).FirstOrDefault();
            mainImage.Url = request.Image.Url;

            await _repository.SaveChangesAsync();

            onSuccess();
        }
    }
}
