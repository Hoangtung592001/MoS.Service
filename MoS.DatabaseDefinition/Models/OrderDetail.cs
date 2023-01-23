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
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double OriginalPrice { get; set; }
        [Required]
        public double FinalPrice { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}
