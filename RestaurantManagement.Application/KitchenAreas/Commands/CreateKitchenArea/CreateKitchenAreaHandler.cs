using MediatR;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.KitchenAreas;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.KitchenAreas.Commands.CreateKitchenArea
{
    public class CreateKitchenAreaHandler : IRequestHandler<CreateKitchenAreaCommand, KitchenAreaDto>
    {
        private readonly IKitchenAreaRepository _areas;
        private readonly IAuditLogService _audit;
        private readonly ICacheService _cache;
        public CreateKitchenAreaHandler(IKitchenAreaRepository areas, IAuditLogService audit, ICacheService cache) { _areas = areas; _audit = audit; _cache = cache; }

        public async Task<KitchenAreaDto> Handle(CreateKitchenAreaCommand request, CancellationToken cancellationToken)
        {
            if (await _areas.NameExistsAsync(request.Name.Trim())) throw new ConflictException("Tên khu vực bếp đã tồn tại");
            var area = new KitchenArea { Name = request.Name.Trim(), Description = request.Description?.Trim(), IsActive = true };
            await _areas.AddAsync(area);
            await _areas.SaveChangesAsync();
            await _audit.AddAsync("CREATE_KITCHEN_AREA", "KitchenArea", area.KitchenAreaId.ToString(), $"Tạo khu vực {area.Name}", cancellationToken);
            await _areas.SaveChangesAsync();
            _cache.RemoveByPrefix("kitchen-areas:");
            return new KitchenAreaDto { KitchenAreaId = area.KitchenAreaId, Name = area.Name, Description = area.Description, IsActive = area.IsActive };
        }
    }
}
