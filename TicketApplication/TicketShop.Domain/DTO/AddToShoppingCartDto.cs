using System;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Domain.DTO
{
    public class AddToShoppingCartDto
    {
        public Product SelectedProduct { get; set; }
        public Guid SelectedProductId { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
    }
}
