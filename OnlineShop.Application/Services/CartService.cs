using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Application.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CartItemResponse> GetCartForUser(Guid userId)
        {
            return _context.Set<CartItem>()
                .Where(ci => ci.UserId == userId)
                .Select(ci => new CartItemResponse
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price,
                    Quantity = ci.Quantity
                })
                .ToList();
        }

        public void AddItem(Guid userId, Guid productId, int quantity)
        {
            var cartItem = _context.Set<CartItem>()
                .FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == productId);

            if (cartItem != null)
            {

                cartItem.Quantity += quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.Set<CartItem>().Add(cartItem);
            }

            _context.SaveChanges();
        }

        public void RemoveItem(Guid userId, Guid productId)
        {
            var cartItem = _context.Set<CartItem>()
                .FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == productId);

            if (cartItem != null)
            {
                _context.Set<CartItem>().Remove(cartItem);
                _context.SaveChanges();
            }
        }
    }
}
