using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.CommonImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.CommonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BookImageTypes;
using static MoS.Models.Constants.Enums.OrderStatus;
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
            var orders = _db.Orders
                            .Include(order => order.OrderDetails)
                                .ThenInclude(orderDetail => orderDetail.Book)
                                    .ThenInclude(book => book.Author)
                            .Include(order => order.OrderDetails)
                                .ThenInclude(orderDetail => orderDetail.Book)
                                    .ThenInclude(book => book.Publisher)
                            .Include(order => order.OrderDetails)
                                .ThenInclude(orderDetail => orderDetail.Book)
                                    .ThenInclude(book => book.BookCondition)
                            .Include(order => order.OrderDetails)
                                .ThenInclude(orderDetail => orderDetail.Book)
                                    .ThenInclude(book => book.BookImages)
                            .Select(order => new Order
                            {
                                Id = order.Id,
                                UserId = order.UserId,
                                OrderStatusId = order.OrderStatusId,
                                CreatedAt = order.CreatedAt,
                                ShippingFee = order.ShippingFee,
                                OrderDetails = order.OrderDetails.Select(
                                        orderDetail => new OrderDetail
                                        {
                                            Id = orderDetail.Id,
                                            OrderId = orderDetail.OrderId,
                                            BookId = orderDetail.BookId,
                                            Quantity = orderDetail.Quantity,
                                            OriginalPrice = orderDetail.OriginalPrice,
                                            FinalPrice = orderDetail.FinalPrice,
                                            Book = new Book
                                            {
                                                Id = orderDetail.Book.Id,
                                                Title = orderDetail.Book.Title,
                                                AuthorId = orderDetail.Book.AuthorId,
                                                PublisherId = orderDetail.Book.PublisherId,
                                                BookConditionId = orderDetail.Book.BookConditionId,
                                                Edition = orderDetail.Book.Edition,
                                                Author = new Author
                                                {
                                                    Id = orderDetail.Book.Author.Id,
                                                    Name = orderDetail.Book.Author.Name
                                                },
                                                Publisher = new Publisher
                                                {
                                                    Id = orderDetail.Book.Publisher.Id,
                                                    Name = orderDetail.Book.Publisher.Name
                                                },
                                                BookImage = new BookImage
                                                {
                                                    Id = orderDetail.Book.BookImages.Where(bookImage => bookImage.BookImageTypeId == (int)BookImageTypeTDs.Main).FirstOrDefault().Id,
                                                    Url = orderDetail.Book.BookImages.Where(bookImage => bookImage.BookImageTypeId == (int)BookImageTypeTDs.Main).FirstOrDefault().Url
                                                },
                                                BookCondition = new BookCondition
                                                {
                                                    Id = orderDetail.Book.BookCondition.Id,
                                                    Name = orderDetail.Book.BookCondition.Name
                                                }
                                            }
                                        }
                                    ).ToList()
                            });

            return orders;
        }

        public async Task<bool> Set(SetOrderRequest request, Credential credential)
        {
            var orderId = Guid.NewGuid();

            _db.Orders.Add(
                    new DatabaseDefinition.Models.Order
                    {
                        Id = orderId,
                        UserId = credential.Id,
                        OrderStatusId = (int) OrderStatusIDs.PREPARING,
                        ShippingFee = 0
                    }
                );

            var bookIDs = request.OrderDetails.Select(orderDetail => orderDetail.BookId);

            // Get All Books With bookIDs and Join Using For

            var books = _db.Books.Where(book => bookIDs.Contains(book.Id)).ToList();

            var requestOrderDetails = request.OrderDetails;

            var orderDetails = (
                    from book in books
                    join orderDetail in requestOrderDetails on book.Id equals orderDetail.BookId
                    select new DatabaseDefinition.Models.OrderDetail
                    {
                        Id = book.Id,
                        OrderId = orderId,
                        BookId = book.Id,
                        Quantity = orderDetail.Quantity,
                        OriginalPrice = book.Price,
                        FinalPrice = new CommonService(new CommonImplementation()).SellOfPrice(book.Price, book.SellOffRate)
                    }
                );

            await _db.OrderDetails.AddRangeAsync(orderDetails);

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
