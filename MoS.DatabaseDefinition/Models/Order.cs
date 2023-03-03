using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.DatabaseDefinition.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public Guid? AddressId { get; set; }
        [Required]
        public double ShippingFee { get; set; } = 0;
        public int OrderStatusId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Address Address { get; set; }
    }
}
