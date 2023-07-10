using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> getAllOrders();

        Order getOrderDetails(BaseEntity model);
    }
}
