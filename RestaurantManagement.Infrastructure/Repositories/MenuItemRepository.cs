using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;

        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<MenuItem>> GetAllAsync() =>
            _context.MenuItems.AsNoTracking().Include(x => x.KitchenArea).OrderBy(x => x.Name).ToListAsync();

        public Task<List<MenuItem>> GetByCategoryAsync(string category) =>
            _context.MenuItems.AsNoTracking().Include(x => x.KitchenArea)
                .Where(x => x.Category == category).OrderBy(x => x.Name).ToListAsync();

        public Task<MenuItem?> GetByIdAsync(int id) =>
            _context.MenuItems.Include(x => x.KitchenArea).FirstOrDefaultAsync(x => x.MenuItemId == id);

        public async Task AddAsync(MenuItem item)
        {
            await _context.MenuItems.AddAsync(item);
        }

        public Task UpdateAsync(MenuItem item)
        {
            _context.MenuItems.Update(item);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(MenuItem item)
        {
            _context.MenuItems.Remove(item);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
