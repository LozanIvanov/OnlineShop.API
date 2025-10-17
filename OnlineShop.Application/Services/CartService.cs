using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<CartItem>GetAllItems()=>_context.CartItems.ToList();

        public void AddItem(CartItem item)
        {
            _context.CartItems.Add(item);   
            _context.SaveChanges(); 
        }
        public void RemoveItem(Guid id)
        {
            var item=_context.CartItems.Find(id);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
