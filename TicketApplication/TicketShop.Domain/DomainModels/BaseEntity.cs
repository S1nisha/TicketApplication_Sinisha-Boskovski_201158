using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicketShop.Domain.DomainModels
{
    public class BaseEntity
    {
        [Key]
        public  Guid Id { get; set; }
    }
}
