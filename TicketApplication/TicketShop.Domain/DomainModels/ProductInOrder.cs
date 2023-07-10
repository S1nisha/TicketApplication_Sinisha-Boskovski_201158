using System;

namespace TicketShop.Domain.DomainModels
{
    public class ProductInOrder : BaseEntity
    {
        public  Guid ProductId { get; set; }

        public Product OrderedProduct { get; set; }

        public Guid OrderId { get; set; }

        public Order UserOrder { get; set; }

        public int Quantity { get; set; }
    }
}
