using System;
using System.ComponentModel.DataAnnotations;

namespace MoS.DatabaseDefinition.Models
{
    public class BookImage
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int BookImageTypeId { get; set; }
        public BookImageType BookImageType { get; set; }
    }
}
