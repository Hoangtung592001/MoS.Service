using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.DatabaseDefinition.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PaymentOptionId { get; set; }
        public double Amount { get; set; }
        public PaymentOption PaymentOption { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
