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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .ToListAsync();
        }
        public async Task<bool> ExistsByNameAsync(
           string name)
        {
            return await _context.Categories
                .AnyAsync(x => x.Name == name);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories
                .AddAsync(category);

            await _context.SaveChangesAsync();
        }
        public async Task<Category?> GetByIdAsync(
           int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(x =>
                    x.CategoryId == id);
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }
        public async Task<List<Category>> SearchByNameAsync(
    string keyword)
        {
            return await _context.Categories
                .Where(x => x.Name.Contains(keyword))
                .ToListAsync();
        }
    }
}
