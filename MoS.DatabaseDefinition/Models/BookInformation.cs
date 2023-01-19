using System;
using System.ComponentModel.DataAnnotations;

namespace MoS.DatabaseDefinition.Models
{
    public class BookInformation
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int BookConditionId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double SellOffRate { get; set; }
        [Required]
        public int Edition { get; set; }
        public BookCondition BookCondition { get; set; }
        public int NumberOfViews { get; set; } = 0;
        public string BookDetails { get; set; }
    }
}
