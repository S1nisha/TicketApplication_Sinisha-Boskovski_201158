using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Repository.Interface;

namespace TicketShop.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entitites;
        string errorMessage= string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entitites = context.Set<Order>();
        }
        
        public Order getOrderDetails(BaseEntity model)
        {
            return entitites.Include(o => o.User)
                .Include(o => o.ProductInOrders)
                .Include("ProductInOrders.OrderedProduct")
                .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }

        public List<Order> getAllOrders()
        {
            return entitites.Include(z => z.User)
                .Include(z => z.ProductInOrders)
                .Include("Product.OrderedProduct")
                .ToListAsync().Result;
        }
    }
}
