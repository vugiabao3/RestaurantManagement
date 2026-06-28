using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.AuditLogs.Queries.SearchAuditLogs
{
    public class SearchAuditLogsHandler : IRequestHandler<SearchAuditLogsQuery, List<AuditLogDto>>
    {
        private readonly IAuditLogRepository _repository;
        public SearchAuditLogsHandler(IAuditLogRepository repository) => _repository = repository;

        public async Task<List<AuditLogDto>> Handle(SearchAuditLogsQuery request, CancellationToken cancellationToken)
        {
            if (request.From.HasValue && request.To.HasValue && request.From > request.To)
                throw new BusinessException("Thời gian bắt đầu không được lớn hơn thời gian kết thúc");
            return (await _repository.SearchAsync(request.From, request.To, request.UserId, request.Action, request.Take))
                .Select(x => new AuditLogDto
                {
                    AuditLogId = x.AuditLogId, UserId = x.UserId, Action = x.Action,
                    EntityName = x.EntityName, EntityId = x.EntityId, Description = x.Description,
                    CreatedAt = x.CreatedAt, IpAddress = x.IpAddress
                }).ToList();
        }
    }
}
