using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.DatabaseDefinition.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        public Guid? BasketItemId { get; set; }
        [Required]
        public double OriginalPrice { get; set; }
        [Required]
        public double FinalPrice { get; set; }
        public Order Order { get; set; }
        public BasketItem BasketItem { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
