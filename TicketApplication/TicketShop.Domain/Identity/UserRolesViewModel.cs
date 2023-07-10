using System;
using System.Collections.Generic;
using System.Text;

namespace TicketShop.Domain.Identity
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
