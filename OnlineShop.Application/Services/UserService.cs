using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAllUsers()=>_context.Users.ToList();

        public void AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
