using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces
{
    public  interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();

        Task<User?> GetUserByIdAsync(int id);
        Task<int> CreateUserAsync(User user);
    }
}
