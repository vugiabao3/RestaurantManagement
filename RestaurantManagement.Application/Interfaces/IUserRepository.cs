using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> UsernameExistsAsync(string username);

        Task AddAsync(User user);

        Task<User?> GetByEmailAsync(string email);
        Task UpdateAsync(User user);
        Task<User?> GetByIdAsync(int id);


    }
}