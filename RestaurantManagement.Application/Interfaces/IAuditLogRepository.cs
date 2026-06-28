using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<List<AuditLog>> SearchAsync(DateTime? from, DateTime? to, int? userId, string? action, int take = 200);
    }
}
