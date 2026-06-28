using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Analytics.Queries.GetKitchenAnalytics;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class KitchenAnalyticsRepository : IKitchenAnalyticsRepository
    {
        private readonly AppDbContext _context;

        public KitchenAnalyticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<KitchenAnalyticsResponse> GetSummaryAsync(DateTime from, DateTime to, int? kitchenAreaId)
        {
            var orders = _context.Orders.AsNoTracking()
                .Where(x => x.Created >= from && x.Created <= to);

            if (kitchenAreaId.HasValue)
            {
                orders = orders.Where(o => o.OrderDetails.Any(d => d.MenuItem!.KitchenAreaId == kitchenAreaId.Value));
            }

            var totalOrders = await orders.CountAsync();
            var readyOrders = await orders.CountAsync(x => x.Status == OrderStatus.Ready || x.Status == OrderStatus.Completed);
            var cancelledOrders = await orders.CountAsync(x => x.Status == OrderStatus.Cancelled);

            var completedQuery = _context.OrderDetails.AsNoTracking()
                .Where(x => x.Order!.Created >= from && x.Order.Created <= to && x.Status == OrderStatus.Ready);
            if (kitchenAreaId.HasValue)
                completedQuery = completedQuery.Where(x => x.MenuItem!.KitchenAreaId == kitchenAreaId.Value);

            var completedItems = await completedQuery.SumAsync(x => (int?)x.Quantity) ?? 0;

            var completedTimes = await orders
                .Where(x => x.Completed.HasValue)
                .Select(x => new { x.Created, Completed = x.Completed!.Value })
                .ToListAsync();

            var areaQuery = _context.OrderDetails.AsNoTracking()
                .Where(x => x.Order!.Created >= from && x.Order.Created <= to);
            if (kitchenAreaId.HasValue)
                areaQuery = areaQuery.Where(x => x.MenuItem!.KitchenAreaId == kitchenAreaId.Value);

            var areaRows = await areaQuery
                .Select(x => new
                {
                    x.OrderId,
                    x.MenuItem!.KitchenAreaId,
                    AreaName = x.MenuItem.KitchenArea != null ? x.MenuItem.KitchenArea.Name : "Chưa phân khu"
                })
                .ToListAsync();

            var ordersByArea = areaRows
                .GroupBy(x => new { x.KitchenAreaId, x.AreaName })
                .Select(g => new KitchenAreaOrderCountDto
                {
                    KitchenAreaId = g.Key.KitchenAreaId,
                    KitchenAreaName = g.Key.AreaName,
                    OrderCount = g.Select(x => x.OrderId).Distinct().Count()
                })
                .OrderBy(x => x.KitchenAreaName)
                .ToList();

            var topQuery = _context.OrderDetails.AsNoTracking()
                .Where(x => x.Order!.Created >= from && x.Order.Created <= to);
            if (kitchenAreaId.HasValue)
                topQuery = topQuery.Where(x => x.MenuItem!.KitchenAreaId == kitchenAreaId.Value);

            var topRows = await topQuery
                .Select(x => new { x.MenuItemId, MenuItemName = x.MenuItem!.Name, x.Quantity })
                .ToListAsync();

            var topItems = topRows
                .GroupBy(x => new { x.MenuItemId, x.MenuItemName })
                .Select(g => new TopMenuItemDto
                {
                    MenuItemId = g.Key.MenuItemId,
                    MenuItemName = g.Key.MenuItemName,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.Quantity)
                .Take(5)
                .ToList();

            return new KitchenAnalyticsResponse
            {
                From = from,
                To = to,
                TotalOrders = totalOrders,
                ReadyOrders = readyOrders,
                CancelledOrders = cancelledOrders,
                CompletedItems = completedItems,
                AveragePreparationMinutes = completedTimes.Count == 0
                    ? 0
                    : Math.Round(completedTimes.Average(x => (x.Completed - x.Created).TotalMinutes), 2),
                TopMenuItems = topItems,
                OrdersByArea = ordersByArea
            };
        }
    }
}
