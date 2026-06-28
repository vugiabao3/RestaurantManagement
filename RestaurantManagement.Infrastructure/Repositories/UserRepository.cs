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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(
            string username)
        {
            return await _context.User
                .FirstOrDefaultAsync(x =>
                    x.Username == username);
        }

        public async Task<bool> UsernameExistsAsync(
            string username)
        {
            return await _context.User
                .AnyAsync(x => x.Username == username);
        }

        public async Task AddAsync(User admin)
        {
            await _context.User.AddAsync(admin);

            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetByEmailAsync(
     string email)
        {
            return await _context.User
                .FirstOrDefaultAsync(
                    x => x.Email == email);
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.User
                .FirstOrDefaultAsync(x =>
                    x.UserId == id);
        }
        public async Task UpdateAsync(User admin)
        {
            _context.User.Update(admin);

            await _context.SaveChangesAsync();
        }

    }
}