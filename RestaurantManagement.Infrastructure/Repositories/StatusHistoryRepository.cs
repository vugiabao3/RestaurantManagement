using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class StatusHistoryRepository : IStatusHistoryRepository
    {
        private readonly AppDbContext _context;

        public StatusHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<StatusHistory>> GetByOrderIdAsync(int orderId) =>
            _context.StatusHistories
                .Where(x => x.OrderId == orderId)
                .OrderBy(x => x.ChangedAt)
                .ToListAsync();
    }
}
