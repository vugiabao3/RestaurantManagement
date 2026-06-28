using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public AuditLogService(AppDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task AddAsync(string action, string entityName, string? entityId, string? description, CancellationToken cancellationToken = default)
        {
            await _context.AuditLogs.AddAsync(new AuditLog
            {
                UserId = _currentUser.UserId,
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                Description = description,
                CreatedAt = DateTime.Now,
                IpAddress = _currentUser.IpAddress
            }, cancellationToken);
        }
    }
}
