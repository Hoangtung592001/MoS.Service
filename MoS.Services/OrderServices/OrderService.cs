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

        public class OrderDetail
        {
            public Guid Id { get; set; }
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

        public interface IOrder
        {
            Task Get(Credential credential);
            Task Set();
        }

        
    }
}
