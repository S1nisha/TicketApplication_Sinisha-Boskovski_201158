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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository, IRepository<ProductInOrder> productInOrderRepository, IUserRepository userRepository, IMailService mailService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
            _userRepository = userRepository;
            _mailService = mailService;
        }

        public bool deleteProductFromShoppingCart(string userId, Guid Id)
        {
            if(!string.IsNullOrEmpty(userId) && Id !=null )
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserShoppingCart;
                
                var itemToDelete= userShoppingCart.ProductInShoppingCarts.Where(z=>z.ProductId.Equals(Id)).FirstOrDefault();

                userShoppingCart.ProductInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }
        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userShoppingCart = loggedInUser.UserShoppingCart;

            var allTickets = userShoppingCart.ProductInShoppingCarts.ToList();

            var allTicketsPrice = allTickets.Select(z=>new
                {
                    ProductPrice=z.Product.Price,
                    Quantity=z.Quantity
                }).ToList();

            double totalPrice = 0;

            foreach(var item in allTicketsPrice)
            {
                totalPrice += item.ProductPrice * item.Quantity;
            }

            ShoppingCartDto scDto = new ShoppingCartDto
            {
                ProductsInShoppingCart = allTickets,
                TotalPrice = totalPrice
            };

            return scDto;
        }
        
        public bool orderNow(string userId)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserShoppingCart;

                MailRequest mail = new MailRequest();
                mail.ToEmail=loggedInUser.Email;
                mail.Subject = "Successfully Created Order";

                Order orderItem = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    User = loggedInUser
                };

                this._orderRepository.Insert(orderItem);

                List<ProductInOrder> productInOrders = new List<ProductInOrder>();

                var result = userShoppingCart.ProductInShoppingCarts.Select(z=>new ProductInOrder
                    {
                        Id=Guid.NewGuid(),
                        OrderId=orderItem.Id,
                        ProductId=z.Product.Id,
                        OrderedProduct=z.Product,
                        UserOrder=orderItem,
                        Quantity=z.Quantity
                    }).ToList();

                StringBuilder sb=new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is complete.The Total is:");

                for(int i=1;i<result.Count(); i++)
                {
                    var item = result[i - 1];

                    totalPrice += item.Quantity * item.OrderedProduct.Price;

                    sb.AppendLine(i.ToString()+ " " + item.OrderedProduct.Movie + "Price:" + item.OrderedProduct.Price +" Quantity:"+ item.Quantity);

                    sb.AppendLine("Total Price is: " + totalPrice.ToString());

                    mail.Body= sb.ToString();

                    productInOrders.AddRange(result);

                    foreach(var productitem in productInOrders)
                    {
                        this._productInOrderRepository.Insert(productitem);
                    }
                    loggedInUser.UserShoppingCart.ProductInShoppingCarts.Clear();

                    this._userRepository.Update(loggedInUser);
                    this._mailService.SendEmailAsync(mail);
                    return true;
                }
                return false;
            }
            else
            {
                throw new Exception("Error");
            }
        }
    }
}
