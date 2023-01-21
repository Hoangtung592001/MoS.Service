using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Exception;

namespace MoS.Services.BookServices
{
    public class CreateBookService
    {
        private readonly ICreateBook _repository;

        public CreateBookService(ICreateBook repository)
        {
            _repository = repository;
        }

        public class BookImage
        {
            public string Url { get; set; }
            public int BookImageTypeId { get; set; }
        }

        public class CreateBookRequest
        {
            public Guid AuthorId { get; set; }
            public Guid PulisherId { get; set; }
            public DateTime PublishedAt { get; set; }
            public string Title { get; set; }
            public List<BookImage> Images { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public int Edition { get; set; }
            public int BookConditionId { get; set; }
            public string BookDetails { get; set; }
        }

        public interface ICreateBook
        {
            Task Create(CreateBookRequest book, Action onSuccess, Action<CreateBookExceptionMessage> onFail);
        }

        public async Task Create(CreateBookRequest book, Action onSuccess, Action<CreateBookExceptionMessage> onFail)
        {
            await _repository.Create(book, onSuccess, onFail);
        }
    }
}
