using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.MenuItems.Queries.GetMenuItemsByCategory
{
    public class GetMenuItemsByCategoryHandler : IRequestHandler<GetMenuItemsByCategoryQuery, List<MenuItemDto>>
    {
        private readonly IMenuItemRepository _repository;
        private readonly ICacheService _cache;

        public GetMenuItemsByCategoryHandler(IMenuItemRepository repository, ICacheService cache)
        { _repository = repository; _cache = cache; }

        public Task<List<MenuItemDto>> Handle(GetMenuItemsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var key = $"menu:items:{request.Category ?? "all"}";
            return _cache.GetOrCreateAsync(key, async () =>
            {
                var items = string.IsNullOrWhiteSpace(request.Category)
                    ? await _repository.GetAllAsync()
                    : await _repository.GetByCategoryAsync(request.Category);
                return items.Select(x => new MenuItemDto
                {
                    MenuItemId = x.MenuItemId, Name = x.Name, Category = x.Category,
                    Price = x.Price, IsAvailable = x.IsAvailable,
                    KitchenAreaId = x.KitchenAreaId, KitchenAreaName = x.KitchenArea?.Name
                }).ToList();
            }, TimeSpan.FromSeconds(30));
        }
    }
}
