using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin?> GetByUsernameAsync(string username);
        Task<bool> UsernameExistsAsync(string username);

        Task AddAsync(Admin admin);
    }
}