using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.CommonImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.CommonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Services.OrderServices.OrderService;

namespace MoS.Implementations.OrderImplementations
{
    public class OrderImplementation : IOrder
    {
        private readonly IApplicationDbContext _db;

        public OrderImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Order>> Get(Credential credential)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Set(SetOrderRequest request, Credential credential)
        {
            var orderId = Guid.NewGuid();

            _db.Orders.Add(
                    new DatabaseDefinition.Models.Order
                    {
                        Id = orderId,
                        UserId = credential.Id,
                        ReceiverName = request.ReceiverName,
                        PhoneNumber = request.PhoneNumber,
                        Address = request.Address,
                        Longtitude = request.Longtitude,
                        Latitude = request.Latitude
                    }
                );

            var bookIDs = request.OrderDetails.Select(orderDetail => orderDetail.BookId);
            var orderDetails = request.OrderDetails;
            var books = (
                    from book in _db.Books
                    join orderDetail in orderDetails on book.Id equals orderDetail.BookId
                    select new DatabaseDefinition.Models.OrderDetail
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        BookId = book.Id,
                        Quantity = orderDetail.Quantity,
                        OriginalPrice = book.Price,
                        FinalPrice = new CommonService(new CommonImplementation()).SellOfPrice(book.Price, book.SellOffRate)
                    }
                );

            await _db.OrderDetails.AddRangeAsync(books);

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
