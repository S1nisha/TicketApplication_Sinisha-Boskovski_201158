using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketShop.Domain.DomainModels
{
    public class Product : BaseEntity
    {
        public string Movie { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime ValidTime { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public Genre Genre { get; set; }
        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual ICollection<ProductInOrder> Orders { get; set; }
    }
}