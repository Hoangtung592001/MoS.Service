using System;
using System.ComponentModel.DataAnnotations;

namespace MoS.DatabaseDefinition.Models
{
    public class BookDetail
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Data { get; set; }
    }
}
