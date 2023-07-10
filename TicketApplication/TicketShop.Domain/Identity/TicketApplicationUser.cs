using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Domain.Identity
{
    public class TicketApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ShoppingCart UserShoppingCart { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
