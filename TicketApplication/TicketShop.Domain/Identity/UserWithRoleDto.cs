using System;
using System.Collections.Generic;
using System.Text;

namespace TicketShop.Domain.Identity
{
    public class UserWithRoleDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
    }
}
