using OnlineShop.Application.DTOs;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<OrderDTO> GetOrdersByUser(Guid userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    CreatedAt = o.CreatedAt,
                    TotalAmount = o.TotalAmount,
                    Items = o.Items.Select(oi => new OrderItemDTO
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        ProductPrice = oi.Price,
                        Quantity = oi.Quantity
                    }).ToList()
                })
                .ToList();
        }

        public OrderDTO CreateOrder(Guid userId)
        {
            var cartItems = _context.Set<CartItem>()
                .Where(c => c.UserId == userId)
                .ToList();

            if (!cartItems.Any())
                throw new Exception("Cart is empty!");

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                TotalAmount = cartItems.Sum(c => c.Product.Price * c.Quantity)
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            var orderItemDTOs = new List<OrderItemDTO>();

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };

                _context.OrderItems.Add(orderItem);

                orderItemDTOs.Add(new OrderItemDTO
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    ProductPrice = item.Product.Price,
                    Quantity = item.Quantity
                });
            }

            _context.Set<CartItem>().RemoveRange(cartItems);
            _context.SaveChanges();

            return new OrderDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                TotalAmount = order.TotalAmount,
                Items = orderItemDTOs
            };
        }
    }
}
