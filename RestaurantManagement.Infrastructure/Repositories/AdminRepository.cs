using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<Admin?> GetByUsernameAsync(
            string username)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(x =>
                    x.Username == username);
        }

        public async Task<bool> UsernameExistsAsync(
            string username)
        {
            return await _context.Admins
                .AnyAsync(x => x.Username == username);
        }

        public async Task AddAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);

            await _context.SaveChangesAsync();
        }
    }
}