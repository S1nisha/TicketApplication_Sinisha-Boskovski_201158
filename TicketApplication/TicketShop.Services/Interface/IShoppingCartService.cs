using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DTO;

namespace TicketShop.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);

        bool deleteProductFromShoppingCart(string userId, Guid Id);

        bool orderNow(String userId);
    }
}
