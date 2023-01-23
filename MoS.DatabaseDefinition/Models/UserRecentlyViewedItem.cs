using System;
using System.ComponentModel.DataAnnotations;

namespace MoS.DatabaseDefinition.Models
{
    public class UserRecentlyViewedItem
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid BookId { get; set; }
        public DateTime ViewedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
    }
}
