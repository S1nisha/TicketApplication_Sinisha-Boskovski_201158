using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Services.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders(string userId);

        Order GetOrderDetails(BaseEntity model);
    }
}
