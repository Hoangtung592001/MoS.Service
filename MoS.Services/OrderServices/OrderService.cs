using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            public int Id { get; set; }
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
            public bool IsDeleted { get; set; }
        }

        public class BasketItem
        {
            public Guid Id { get; set; }
            public Guid BookId { get; set; }
            public Guid UserId { get; set; }
            public int Quantity { get; set; }
            public Book Book { get; set; }
        }

        public class OrderDetail
        {
            public Guid Id { get; set; }
            public Guid OrderId { get; set; }
            public Guid? BasketItemId { get; set; }
            public double OriginalPrice { get; set; }
            public double FinalPrice { get; set; }
            public BasketItem BasketItem { get; set; }
        }

        public class OrderStatus
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Order
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public int OrderStatusId { get; set; }
            public Guid? AddressId { get; set; }
            public double ShippingFee { get; set; }
            public OrderStatus OrderStatus { get; set; }
            public DateTime CreatedAt { get; set; }
            public IEnumerable<OrderDetail> OrderDetails { get; set; }
        }

        public class SetOrderRequest
        {
            public List<Guid> BasketItemIDs { get; set; }
            public Guid AddressId { get; set; }
            public Guid PaymentOptionId { get; set; }
        }

        public interface IOrder
        {
            IEnumerable<Order> Get(Credential credential);
            Task Set(SetOrderRequest request, Credential credential, Action<Guid> onSuccess, Action<Guid> onFail);
        }

        public IEnumerable<Order> Get(Credential credential)
        {
            return _repository.Get(credential);
        }

        public async Task Set(SetOrderRequest request, Credential credential, Action<Guid> onSuccess, Action<Guid> onFail)
        {
            await _repository.Set(request, credential, onSuccess, onFail);
        }
    }
}
