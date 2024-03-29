﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BasketItemTypeDescription;

namespace MoS.DatabaseDefinition.Models
{
    public class BasketItem
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        public int? BasketItemTypeDescriptionId { get; set; } = (int) BasketItemTypeDescriptionIDs.InBasket;
        public BasketItemTypeDescription BasketItemTypeDescription { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
