using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public Guid BookInformationId { get; set; }
        [Required]
        public DateTime PublishedAt { get; set; }
        [Required]
        public DateTime StartedToSellAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public BookInformation BookInformation { get; set; }
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        public List<BookImage> BookImages { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedAt { get; set; }
        public Guid DeletedBy { get; set; }
    }
}
