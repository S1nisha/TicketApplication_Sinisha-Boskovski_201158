using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.DTO;

namespace TicketShop.Services.Interface
{
    public interface IProductService
    {
        List<Product> GetValidProducts();
        List<Product> GetAllProducts();

        Product GetDetailsForProduct(Guid? id);
        void CreateNewProduct(Product p);

        void UpdateExistingProduct(Product p);
        AddToShoppingCartDto GetShoppingCartInfo(Guid? id);

        void DeleteProduct(Guid? id);

        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);
        
    }
}
