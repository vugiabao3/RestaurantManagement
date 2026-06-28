using MediatR;

namespace RestaurantManagement.Application.AuditLogs.Queries.SearchAuditLogs
{
    public class SearchAuditLogsQuery : IRequest<List<AuditLogDto>>
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? UserId { get; set; }
        public string? Action { get; set; }
        public int Take { get; set; } = 200;
    }

    public class AuditLogDto
    {
        public long AuditLogId { get; set; }
        public int? UserId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string? EntityId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? IpAddress { get; set; }
    }
}
