using System;
using System.ComponentModel.DataAnnotations;

namespace MoS.DatabaseDefinition.Models
{
    public class BookCondition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
