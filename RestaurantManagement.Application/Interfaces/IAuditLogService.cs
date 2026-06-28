namespace RestaurantManagement.Application.Interfaces
{
    public interface IAuditLogService
    {
        Task AddAsync(string action, string entityName, string? entityId, string? description, CancellationToken cancellationToken = default);
    }
}
