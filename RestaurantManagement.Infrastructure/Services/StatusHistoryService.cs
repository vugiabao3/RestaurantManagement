using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Services
{
    public class StatusHistoryService : IStatusHistoryService
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public StatusHistoryService(AppDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task AddAsync(int? orderId, int? orderDetailId, string oldStatus, string newStatus, string? reason = null, CancellationToken cancellationToken = default)
        {
            await _context.StatusHistories.AddAsync(new StatusHistory
            {
                OrderId = orderId,
                OrderDetailId = orderDetailId,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedByUserId = _currentUser.UserId,
                ChangedAt = DateTime.Now,
                Reason = reason
            }, cancellationToken);
        }
    }
}
