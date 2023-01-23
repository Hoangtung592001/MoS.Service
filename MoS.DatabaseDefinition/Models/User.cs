using System;
using System.ComponentModel.DataAnnotations;

namespace MoS.DatabaseDefinition.Models
{
    public class User
	{
		[Key]
		public Guid Id { get; set; }

		[Required, MaxLength(50)]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public int RoleId { get; set; }

		public virtual Role Role { get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.Now;

		public DateTime? ChangedPasswordDate { get; set; }
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }
		public Guid? DeletedBy { get; set; }
	}
}
