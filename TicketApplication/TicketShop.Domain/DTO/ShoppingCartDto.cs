using System.Collections.Generic;
using TicketShop.Domain.DomainModels;
//using TicketShop.Web.Models.Domainn.Relationship;

namespace TicketShop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<ProductInShoppingCart> ProductsInShoppingCart { get; set; }
        public double TotalPrice { get; set; }
    }
}
