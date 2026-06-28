using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.MenuItems.Queries.GetMenuCategories
{
    public class GetMenuCategoriesHandler : IRequestHandler<GetMenuCategoriesQuery, List<MenuCategorySummaryDto>>
    {
        private readonly IMenuItemRepository _repository;
        private readonly ICacheService _cache;

        public GetMenuCategoriesHandler(IMenuItemRepository repository, ICacheService cache)
        { _repository = repository; _cache = cache; }

        public Task<List<MenuCategorySummaryDto>> Handle(GetMenuCategoriesQuery request, CancellationToken cancellationToken) =>
            _cache.GetOrCreateAsync("menu:categories", async () =>
                (await _repository.GetAllAsync()).GroupBy(x => x.Category)
                    .Select(g => new MenuCategorySummaryDto { Category = g.Key, ItemCount = g.Count() })
                    .OrderBy(x => x.Category).ToList(), TimeSpan.FromSeconds(30));
    }
}
