using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.DatabaseDefinition.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public int PhoneCode { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
