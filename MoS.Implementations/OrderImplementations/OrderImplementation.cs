using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using MoS.Implementations.CommonImplementations;
using MoS.Models.CommonUseModels;
using MoS.Services.CommonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BasketItemTypeDescription;
using static MoS.Models.Constants.Enums.BookImageTypes;
using static MoS.Models.Constants.Enums.OrderStatus;
using static MoS.Services.OrderServices.OrderService;
using static MoS.Services.ShippingServices.ShippingService;
using static MoS.Models.Constants.Enums.Exception;
using static MoS.Services.OrderServices.GenerateOrderNumberService;

namespace MoS.Implementations.OrderImplementations
{
    public class OrderImplementation : IOrder
    {
        private readonly IApplicationDbContext _db;
        private readonly IShipping _shippingService;
        private readonly IGenerateOrderNumber _generateOrderNumberService;

        public OrderImplementation(IApplicationDbContext db, IShipping shippingService)
        {
            _db = db;
            _shippingService = shippingService;
        }

        public OrderImplementation(IApplicationDbContext db, IShipping shippingService, IGenerateOrderNumber generateOrderNumberService)
        {
            _db = db;
            _shippingService = shippingService;
            _generateOrderNumberService = generateOrderNumberService;
        }

        public OrderImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Order> Get(Credential credential)
        {
            var orders = _db.Orders
                            .Include(order => order.OrderDetails)
                            .Include(order => order.OrderDetails)
                                .ThenInclude(orderDetail => orderDetail.BasketItem)
                            .Include(order => order.OrderDetails)
                                .ThenInclude(orderDetail => orderDetail.BasketItem)
                                    .ThenInclude(basketItem => basketItem.Book)
                            .Include(order => order.OrderDetails)
                                .ThenInclude(orderDetail => orderDetail.BasketItem)
                                    .ThenInclude(basketItem => basketItem.Book)
                                        .ThenInclude(book => book.Author)
                            .Include(order => order.OrderDetails)
                                .ThenInclude(orderDetail => orderDetail.BasketItem)
                                    .ThenInclude(basketItem => basketItem.Book)
                                        .ThenInclude(book => book.Publisher)
                            .Include(order => order.OrderDetails)
                                .ThenInclude(orderDetail => orderDetail.BasketItem)
                                    .ThenInclude(basketItem => basketItem.Book)
                                        .ThenInclude(book => book.BookCondition)
                            .Include(order => order.OrderDetails)
                                .ThenInclude(orderDetail => orderDetail.BasketItem)
                                    .ThenInclude(basketItem => basketItem.Book)
                                        .ThenInclude(book => book.BookImages)
                            .Include(order => order.Address)
                            .Where(order => order.IsDeleted == false &&
                                            order.UserId.Equals(credential.Id) &&
                                            order.OrderDetails.All(od => od.IsDeleted == false))
                            .OrderByDescending(order => order.CreatedAt)
                            .Select(order => new Order
                            {
                                Id = order.Id,
                                UserId = order.UserId,
                                OrderStatusId = order.OrderStatusId,
                                CreatedAt = order.CreatedAt,
                                AddressId = order.AddressId,
                                ShippingFee = order.ShippingFee,
                                OrderStatus = new OrderStatus
                                {
                                    Id = order.OrderStatus.Id,
                                    Name = order.OrderStatus.Name
                                },
                                OrderDetails = order.OrderDetails
                                        .Select(
                                        orderDetail => new OrderDetail
                                        {
                                            Id = orderDetail.Id,
                                            OrderId = orderDetail.OrderId,
                                            OriginalPrice = orderDetail.OriginalPrice,
                                            FinalPrice = orderDetail.FinalPrice,
                                            BasketItemId = orderDetail.BasketItemId,
                                            BasketItem = new BasketItem
                                            {
                                                Id = orderDetail.BasketItem.Id,
                                                BookId = orderDetail.BasketItem.BookId,
                                                UserId = orderDetail.BasketItem.UserId,
                                                Quantity = orderDetail.BasketItem.Quantity,
                                                Book = new Book
                                                {
                                                    Id = orderDetail.BasketItem.Book.Id,
                                                    Title = orderDetail.BasketItem.Book.Title,
                                                    AuthorId = orderDetail.BasketItem.Book.AuthorId,
                                                    PublisherId = orderDetail.BasketItem.Book.PublisherId,
                                                    BookConditionId = orderDetail.BasketItem.Book.BookConditionId,
                                                    Edition = orderDetail.BasketItem.Book.Edition,
                                                    Author = new Author
                                                    {
                                                        Id = orderDetail.BasketItem.Book.Author.Id,
                                                        Name = orderDetail.BasketItem.Book.Author.Name
                                                    },
                                                    Publisher = new Publisher
                                                    {
                                                        Id = orderDetail.BasketItem.Book.Publisher.Id,
                                                        Name = orderDetail.BasketItem.Book.Publisher.Name
                                                    },
                                                    BookImage = new BookImage
                                                    {
                                                        Id = orderDetail.BasketItem.Book.BookImages.Where(bookImage => bookImage.BookImageTypeId == (int)BookImageTypeTDs.Main).FirstOrDefault().Id,
                                                        Url = orderDetail.BasketItem.Book.BookImages.Where(bookImage => bookImage.BookImageTypeId == (int)BookImageTypeTDs.Main).FirstOrDefault().Url
                                                    },
                                                    BookCondition = new BookCondition
                                                    {
                                                        Id = orderDetail.BasketItem.Book.BookCondition.Id,
                                                        Name = orderDetail.BasketItem.Book.BookCondition.Name
                                                    },
                                                    IsDeleted = orderDetail.BasketItem.Book.IsDeleted
                                                }
                                            }
                                        }
                                    )
                            }
                            ).AsEnumerable();

            return orders;
        }

        public async Task Set(SetOrderRequest request, Credential credential, Action<string> onSuccess, Action<Guid> onFail)
        {
            var address = _db.Addresses.Where(a => a.Id.Equals(request.AddressId) && a.IsDeleted == false).FirstOrDefault();

            if (address == null)
            {
                onFail(Guid.Empty);
            }

            var shippingFee = _shippingService.Get(address.Id);
            var orderId = Guid.NewGuid();
            var orderNumber = _generateOrderNumberService.Generate();
            var basketItems = await _db.BasketItems
                                .Include(bi => bi.Book)
                                .Where(bi =>
                                    request.BasketItemIDs.Contains(bi.Id) &&
                                    bi.IsDeleted == false &&
                                    bi.Book.IsDeleted == false).ToListAsync();

            foreach(var item in basketItems)
            {
                var selectedBook = _db.Books.Where(b => b.Id.Equals(item.BookId)).FirstOrDefault();

                if (item.Quantity > selectedBook.Quantity)
                {
                    onFail(ChangeQuantityExceptionMessages["QUANTITY_EXCEED"]);
                    return;
                }
            }


            foreach (var item in basketItems)
            {
                var selectedBook = _db.Books.Where(b => b.Id.Equals(item.BookId)).FirstOrDefault();
                selectedBook.Quantity -= item.Quantity;
            }

            foreach (var item in basketItems)
            {
                item.BasketItemTypeDescriptionId = (int) BasketItemTypeDescriptionIDs.Ordered;
            }

            var orderDetails = basketItems.Select(bi => new DatabaseDefinition.Models.OrderDetail
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                OriginalPrice = bi.Book.Price * bi.Quantity,
                FinalPrice = bi.Book.Price * bi.Quantity * (100 - bi.Book.SellOffRate) / 100,
                BasketItemId = bi.Id
            });

            _db.Orders.Add(new DatabaseDefinition.Models.Order
            {
                Id = orderId,
                UserId = credential.Id,
                AddressId = request.AddressId,
                ShippingFee = shippingFee,
                OrderStatusId = (int)OrderStatusIDs.PREPARING,
                OrderNumber = orderNumber
            });

            _db.OrderDetails.AddRange(orderDetails);

            await _db.SaveChangesAsync();

            onSuccess(orderNumber);
        }
    }
}
