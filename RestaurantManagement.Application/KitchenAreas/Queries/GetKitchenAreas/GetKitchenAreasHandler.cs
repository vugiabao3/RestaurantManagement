using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.KitchenAreas;

namespace RestaurantManagement.Application.KitchenAreas.Queries.GetKitchenAreas
{
    public class GetKitchenAreasHandler : IRequestHandler<GetKitchenAreasQuery, List<KitchenAreaDto>>
    {
        private readonly IKitchenAreaRepository _areas;
        private readonly ICacheService _cache;
        public GetKitchenAreasHandler(IKitchenAreaRepository areas, ICacheService cache) { _areas = areas; _cache = cache; }

        public Task<List<KitchenAreaDto>> Handle(GetKitchenAreasQuery request, CancellationToken cancellationToken) =>
            _cache.GetOrCreateAsync("kitchen-areas:all", async () =>
                (await _areas.GetAllAsync()).Select(x => new KitchenAreaDto
                {
                    KitchenAreaId = x.KitchenAreaId, Name = x.Name,
                    Description = x.Description, IsActive = x.IsActive
                }).ToList(), TimeSpan.FromSeconds(30));
    }
}
