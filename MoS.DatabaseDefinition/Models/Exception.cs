using System;
using System.ComponentModel.DataAnnotations;

namespace MoS.DatabaseDefinition.Models
{
    public class Exception
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string ExceptionMessageType { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
