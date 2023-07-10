using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.DTO;
using TicketShop.Repository.Interface;
using TicketShop.Services.Interface;

namespace TicketShop.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInShoppingCart>  _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;

        public ProductService(IRepository<Product> productRepository, IUserRepository userRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository)
        {
            _productRepository = productRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _userRepository = userRepository;
        }

        public List<Product> GetAllProducts()
        {
            return this._productRepository.GetAll().ToList();
        }

        public Product GetDetailsForProduct(Guid? id)
        {
            return this._productRepository.Get(id);
        }

        public void CreateNewProduct(Product p)
        {
            p.Id = Guid.NewGuid();
            this._productRepository.Insert(p);
        }

        public void UpdateExistingProduct(Product p)
        {
            this._productRepository.Update(p);
        }

        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var product = this.GetDetailsForProduct(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                SelectedProduct = product,
                SelectedProductId = product.Id,
                Quantity = 1
            };
            
            return model;
        }
        public List<Product> GetValidProducts()
        {
            var today = DateTime.Now;

            var tickets = this._productRepository.GetAll();

            var validTickets = tickets.Where(z => z.ValidTime >= today).OrderBy(x => x.ValidTime).ToList();

            return validTickets;

        }

        public void DeleteProduct(Guid? id)
        {
            var product=this.GetDetailsForProduct(id);
            this._productRepository.Delete(product);
        }

        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {

            var user = this._userRepository.Get(userID);

            var userShoppingCart = user.UserShoppingCart;

            if (item.SelectedProductId != null && userShoppingCart != null)
            {
                var product = this.GetDetailsForProduct(item.SelectedProductId);
                if (product != null)
                {
                    ProductInShoppingCart itemToAdd = new ProductInShoppingCart
                    {
                        Product = product,
                        ProductId = product.Id,
                        ShoppingCart = userShoppingCart,
                        ShoppingCartId = userShoppingCart.Id,
                        Quantity = item.Quantity
                    };

                    this._productInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        
    }

}
