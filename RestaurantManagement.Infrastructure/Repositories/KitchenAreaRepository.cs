using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class KitchenAreaRepository : IKitchenAreaRepository
    {
        private readonly AppDbContext _context;

        public KitchenAreaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<KitchenArea>> GetAllAsync() =>
            _context.KitchenAreas.OrderBy(x => x.Name).ToListAsync();

        public Task<KitchenArea?> GetByIdAsync(int id) =>
            _context.KitchenAreas.FirstOrDefaultAsync(x => x.KitchenAreaId == id);

        public Task<bool> NameExistsAsync(string name, int? excludeId = null) =>
            _context.KitchenAreas.AnyAsync(x => x.Name == name &&
                (!excludeId.HasValue || x.KitchenAreaId != excludeId.Value));

        public async Task AddAsync(KitchenArea area)
        {
            await _context.KitchenAreas.AddAsync(area);
        }

        public Task DeleteAsync(KitchenArea area)
        {
            _context.KitchenAreas.Remove(area);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
