using System;
using System.ComponentModel.DataAnnotations;

namespace MoS.DatabaseDefinition.Models
{
    public class BookCondition
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Condition { get; set; }
    }
}
