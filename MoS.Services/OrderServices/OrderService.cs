using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.OrderServices
{
    public class OrderService
    {
        private readonly IOrder _repository;

        public OrderService(IOrder repository)
        {
            _repository = repository;
        }

        public class Author
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class BookImage
        {
            public Guid Id { get; set; }
            public string Url { get; set; }
        }

        public class Publisher
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class BookCondition
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class Book
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public Guid AuthorId { get; set; }
            public Guid PublisherId { get; set; }
            public int BookConditionId { get; set; }
            public int Edition { get; set; }
            public Author Author { get; set; }
            public Publisher Publisher { get; set; }
            public BookImage BookImage { get; set; }
            public BookCondition BookCondition { get; set; }
        }

        public class OrderDetail
        {
            public Guid Id { get; set; }
            public Guid OrderId { get; set; }
            public Guid BookId { get; set; }
            public int Quantity { get; set; }
            public double OriginalPrice { get; set; }
            public double FinalPrice { get; set; }
            public Book Book { get; set; }
        }

        public class Order
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public string ReceiverName { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public double Longtitude { get; set; }
            public double Latitude { get; set; }
            public int OrderStatusId { get; set; }
            public DateTime CreatedAt { get; set; }
            public ICollection<OrderDetail> OrderDetails { get; set; }
        }

        public class SetOrderRequest
        {
            public class OrderDetail
            {
                public Guid BookId { get; set; }
                public int Quantity { get; set; }
            }

            public List<OrderDetail> OrderDetails { get; set; }
            public string ReceiverName { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public double Longtitude { get; set; }
            public double Latitude { get; set; }
        }



        public interface IOrder
        {
            Task<IEnumerable<Order>> Get(Credential credential);
            Task<bool> Set(SetOrderRequest request, Credential credential);
        }

        public async Task<IEnumerable<Order>> Get(Credential credential)
        {
            return await _repository.Get(credential);
        }

        public async Task<bool> Set(SetOrderRequest request, Credential credential)
        {
            return await _repository.Set(request, credential);
        }
    }
}
