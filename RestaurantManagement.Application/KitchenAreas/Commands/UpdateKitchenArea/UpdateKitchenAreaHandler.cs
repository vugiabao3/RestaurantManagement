using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.KitchenAreas;

namespace RestaurantManagement.Application.KitchenAreas.Commands.UpdateKitchenArea
{
    public class UpdateKitchenAreaHandler : IRequestHandler<UpdateKitchenAreaCommand, KitchenAreaDto>
    {
        private readonly IKitchenAreaRepository _areas;
        private readonly IAuditLogService _audit;
        private readonly ICacheService _cache;
        public UpdateKitchenAreaHandler(IKitchenAreaRepository areas, IAuditLogService audit, ICacheService cache) { _areas = areas; _audit = audit; _cache = cache; }

        public async Task<KitchenAreaDto> Handle(UpdateKitchenAreaCommand request, CancellationToken cancellationToken)
        {
            var area = await _areas.GetByIdAsync(request.KitchenAreaId) ?? throw new NotFoundException("Không tìm thấy khu vực bếp");
            if (await _areas.NameExistsAsync(request.Name.Trim(), request.KitchenAreaId)) throw new ConflictException("Tên khu vực bếp đã tồn tại");
            area.Name = request.Name.Trim(); area.Description = request.Description?.Trim(); area.IsActive = request.IsActive;
            await _audit.AddAsync("UPDATE_KITCHEN_AREA", "KitchenArea", area.KitchenAreaId.ToString(), $"Cập nhật khu vực {area.Name}", cancellationToken);
            await _areas.SaveChangesAsync();
            _cache.RemoveByPrefix("kitchen-areas:"); _cache.RemoveByPrefix("menu:"); _cache.RemoveByPrefix("pending-orders:");
            return new KitchenAreaDto { KitchenAreaId = area.KitchenAreaId, Name = area.Name, Description = area.Description, IsActive = area.IsActive };
        }
    }
}
