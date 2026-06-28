using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.KitchenAreas.Commands.DeleteKitchenArea
{
    public class DeleteKitchenAreaHandler : IRequestHandler<DeleteKitchenAreaCommand, string>
    {
        private readonly IKitchenAreaRepository _areas;
        private readonly IAuditLogService _audit;
        private readonly ICacheService _cache;
        public DeleteKitchenAreaHandler(IKitchenAreaRepository areas, IAuditLogService audit, ICacheService cache) { _areas = areas; _audit = audit; _cache = cache; }

        public async Task<string> Handle(DeleteKitchenAreaCommand request, CancellationToken cancellationToken)
        {
            var area = await _areas.GetByIdAsync(request.KitchenAreaId) ?? throw new NotFoundException("Không tìm thấy khu vực bếp");
            await _areas.DeleteAsync(area);
            await _audit.AddAsync("DELETE_KITCHEN_AREA", "KitchenArea", area.KitchenAreaId.ToString(), $"Xóa khu vực {area.Name}", cancellationToken);
            await _areas.SaveChangesAsync();
            _cache.RemoveByPrefix("kitchen-areas:"); _cache.RemoveByPrefix("menu:"); _cache.RemoveByPrefix("pending-orders:");
            return "Xóa khu vực bếp thành công";
        }
    }
}
