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
using static MoS.Services.ShippingServices.ShippingService;

namespace MoS.Implementations.OrderImplementations
{
    public class OrderImplementation : IOrder
    {
        private readonly IApplicationDbContext _db;
        private readonly IShipping _shippingService;

        public OrderImplementation(IApplicationDbContext db, IShipping shippingService)
        {
            _db = db;
            _shippingService = shippingService;
        }

        public OrderImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public Task<IEnumerable<Order>> Get(Credential credential)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<Order>> Get(Credential credential)
        //{
        //    var orders = _db.Orders
        //                    .Include(order => order.OrderDetails)
        //                        .ThenInclude(orderDetail => orderDetail.Book)
        //                            .ThenInclude(book => book.Author)
        //                    .Include(order => order.OrderDetails)
        //                        .ThenInclude(orderDetail => orderDetail.Book)
        //                            .ThenInclude(book => book.Publisher)
        //                    .Include(order => order.OrderDetails)
        //                        .ThenInclude(orderDetail => orderDetail.Book)
        //                            .ThenInclude(book => book.BookCondition)
        //                    .Include(order => order.OrderDetails)
        //                        .ThenInclude(orderDetail => orderDetail.Book)
        //                            .ThenInclude(book => book.BookImages)
        //                    .Select(order => new Order
        //                    {
        //                        Id = order.Id,
        //                        UserId = order.UserId,
        //                        OrderStatusId = order.OrderStatusId,
        //                        CreatedAt = order.CreatedAt,
        //                        ShippingFee = order.ShippingFee,
        //                        OrderDetails = order.OrderDetails.Select(
        //                                orderDetail => new OrderDetail
        //                                {
        //                                    Id = orderDetail.Id,
        //                                    OrderId = orderDetail.OrderId,
        //                                    BookId = orderDetail.BookId,
        //                                    Quantity = orderDetail.Quantity,
        //                                    OriginalPrice = orderDetail.OriginalPrice,
        //                                    FinalPrice = orderDetail.FinalPrice,
        //                                    Book = new Book
        //                                    {
        //                                        Id = orderDetail.Book.Id,
        //                                        Title = orderDetail.Book.Title,
        //                                        AuthorId = orderDetail.Book.AuthorId,
        //                                        PublisherId = orderDetail.Book.PublisherId,
        //                                        BookConditionId = orderDetail.Book.BookConditionId,
        //                                        Edition = orderDetail.Book.Edition,
        //                                        Author = new Author
        //                                        {
        //                                            Id = orderDetail.Book.Author.Id,
        //                                            Name = orderDetail.Book.Author.Name
        //                                        },
        //                                        Publisher = new Publisher
        //                                        {
        //                                            Id = orderDetail.Book.Publisher.Id,
        //                                            Name = orderDetail.Book.Publisher.Name
        //                                        },
        //                                        BookImage = new BookImage
        //                                        {
        //                                            Id = orderDetail.Book.BookImages.Where(bookImage => bookImage.BookImageTypeId == (int)BookImageTypeTDs.Main).FirstOrDefault().Id,
        //                                            Url = orderDetail.Book.BookImages.Where(bookImage => bookImage.BookImageTypeId == (int)BookImageTypeTDs.Main).FirstOrDefault().Url
        //                                        },
        //                                        BookCondition = new BookCondition
        //                                        {
        //                                            Id = orderDetail.Book.BookCondition.Id,
        //                                            Name = orderDetail.Book.BookCondition.Name
        //                                        }
        //                                    }
        //                                }
        //                            ).ToList()
        //                    });

        //    return orders;
        //}

        public async Task<bool> Set(SetOrderRequest request, Credential credential)
        {
            var address = _db.Addresses.Where(a => a.Id.Equals(request.AddressId) && a.IsDeleted == false).FirstOrDefault();

            if (address == null)
            {
                return false;
            }

            var shippingFee = _shippingService.Get(address.Id);
            var orderId = Guid.NewGuid();
            var basketItems = await _db.BasketItems
                                .Include(bi => bi.Book)
                                .Where(bi =>
                                    request.BasketItemIDs.Contains(bi.Id) &&
                                    bi.IsDeleted == false &&
                                    bi.Book.IsDeleted == false).ToListAsync();

            var orderDetails = basketItems.Select(bi => new DatabaseDefinition.Models.OrderDetail
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                BookId = bi.Book.Id,
                OriginalPrice = bi.Book.Price,
                FinalPrice = bi.Book.Price * (100 - bi.Book.SellOffRate) / 100,
                BasketItemId = bi.Id
            });

            _db.Orders.Add(new DatabaseDefinition.Models.Order
            {
                Id = orderId,
                UserId = credential.Id,
                AddressId = request.AddressId,
                ShippingFee = shippingFee,
                OrderStatusId = (int)OrderStatusIDs.PREPARING,
            });

            _db.OrderDetails.AddRange(orderDetails);

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
