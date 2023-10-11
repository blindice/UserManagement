using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastrcuture.Repositories
{
    public class UserRepository : IUserRepository
    {
        UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }
    }
}
