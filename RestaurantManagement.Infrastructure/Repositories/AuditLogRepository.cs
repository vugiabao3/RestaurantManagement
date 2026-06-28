using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<AuditLog>> SearchAsync(DateTime? from, DateTime? to, int? userId, string? action, int take = 200)
        {
            var query = _context.AuditLogs.AsNoTracking().AsQueryable();

            if (from.HasValue) query = query.Where(x => x.CreatedAt >= from.Value);
            if (to.HasValue) query = query.Where(x => x.CreatedAt <= to.Value);
            if (userId.HasValue) query = query.Where(x => x.UserId == userId.Value);
            if (!string.IsNullOrWhiteSpace(action)) query = query.Where(x => x.Action == action);

            return query.OrderByDescending(x => x.CreatedAt)
                .Take(Math.Clamp(take, 1, 500))
                .ToListAsync();
        }
    }
}
