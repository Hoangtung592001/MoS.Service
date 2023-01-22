using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.BookServices
{
    public class GetBookService
    {
        private readonly IGetBook _repository;

        public GetBookService(IGetBook repository)
        {
            _repository = repository;
        }

        public class BookImage
        {
            public Guid Id { get; set; }
            public string Url { get; set; }
            public int BookImageTypeId { get; set; }
        }

        public class Author
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class Publisher
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class BookCondition
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Book
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public Guid AuthorId { get; set; }
            public Guid PublisherId { get; set; }
            public DateTime PublishedAt { get; set; }
            public DateTime StartedToSellAt { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public int BookConditionId { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public double SellOffRate { get; set; }
            public int Edition { get; set; }
            public int NumberOfViews { get; set; } = 0;
            public string BookDetails { get; set; }
            public Author Author { get; set; }
            public Publisher Publisher { get; set; }
            public List<BookImage> BookImages { get; set; }
            public BookCondition BookCondition { get; set; }
        }

        public interface IGetBook
        {
            Task<Book> Get(Guid BookId);
        }

        public async Task<Book> Get(Guid BookId)
        {
            return await _repository.Get(BookId);
        }
    }
}
