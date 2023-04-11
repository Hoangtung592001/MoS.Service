using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.BookServices
{
    public class EditBookService
    {
        private readonly IEditBook _repository;

        public EditBookService(IEditBook repository)
        {
            _repository = repository;
        }
        public class BookImage
        {
            public Guid Id { get; set; }
            public string Url { get; set; }
            public int BookImageTypeId { get; set; }
        }

        public class EditBookRequest
        {
            public Guid Id { get; set; }
            public Guid AuthorId { get; set; }
            public Guid PulisherId { get; set; }
            public DateTime PublishedAt { get; set; }
            public string Title { get; set; }
            public BookImage Image { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public int Edition { get; set; }
            public int BookConditionId { get; set; }
            public string Description { get; set; }
            public string BookDetails { get; set; }
        }

        public interface IEditBook
        {
            Task Edit(EditBookRequest request, Action onSuccess, Action onFail);
        }

        public async Task Edit(EditBookRequest request, Action onSuccess, Action onFail)
        {
            await _repository.Edit(request, onSuccess, onFail);
        }
    }
}
