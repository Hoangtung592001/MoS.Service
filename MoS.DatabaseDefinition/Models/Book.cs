using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static MoS.Models.Constants.Enums.BookConditions;

namespace MoS.DatabaseDefinition.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100), Required]
        public string Title { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public Guid PublisherId { get; set; }
        [Required]
        public DateTime PublishedAt { get; set; }
        [Required]
        public DateTime StartedToSellAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public int BookConditionId { get; set; } = (int)BookConditionIDs.Fine;
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double SellOffRate { get; set; }
        [Required]
        public int Edition { get; set; }
        public string Description { get; set; }
        public int NumberOfViews { get; set; } = 0;
        public string BookDetails { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        public List<BookImage> BookImages { get; set; }
        public BookCondition BookCondition { get; set; }
    }
}
