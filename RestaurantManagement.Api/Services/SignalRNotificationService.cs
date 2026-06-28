using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.API.Hubs;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.API.Services
{
    public class SignalRNotificationService : INotificationService
    {
        private readonly IHubContext<KitchenHub> _hub;
        private readonly AppDbContext _context;
        private readonly ILogger<SignalRNotificationService> _logger;

        public SignalRNotificationService(
            IHubContext<KitchenHub> hub,
            AppDbContext context,
            ILogger<SignalRNotificationService> logger)
        {
            _hub = hub;
            _context = context;
            _logger = logger;
        }

        private Task<List<int>> GetOrderAreaIdsAsync(int orderId, CancellationToken cancellationToken) =>
            _context.OrderDetails.AsNoTracking()
                .Where(x => x.OrderId == orderId && x.MenuItem!.KitchenAreaId.HasValue)
                .Select(x => x.MenuItem!.KitchenAreaId!.Value)
                .Distinct()
                .ToListAsync(cancellationToken);

        public async Task<bool> NotifyOrderCreatedAsync(int orderId, CancellationToken cancellationToken = default)
        {
            try
            {
                var areaIds = await GetOrderAreaIdsAsync(orderId, cancellationToken);

                await _hub.Clients.Group("kitchen")
                    .SendAsync("OrderCreated", new { orderId }, cancellationToken);

                foreach (var areaId in areaIds)
                {
                    await _hub.Clients.Group($"kitchen-area-{areaId}")
                        .SendAsync("OrderCreated", new { orderId, areaId }, cancellationToken);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Không thể gửi OrderCreated cho Order {OrderId}", orderId);
                return false;
            }
        }

        public async Task<bool> NotifyOrderUpdatedAsync(int orderId, CancellationToken cancellationToken = default)
        {
            try
            {
                var areaIds = await GetOrderAreaIdsAsync(orderId, cancellationToken);

                await _hub.Clients.Group("kitchen")
                    .SendAsync("OrderUpdated", new { orderId }, cancellationToken);

                foreach (var areaId in areaIds)
                {
                    await _hub.Clients.Group($"kitchen-area-{areaId}")
                        .SendAsync("OrderUpdated", new { orderId, areaId }, cancellationToken);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Không thể gửi OrderUpdated cho Order {OrderId}", orderId);
                return false;
            }
        }

        public async Task<bool> NotifyOrderDetailStatusChangedAsync(
            int orderId,
            int orderDetailId,
            string status,
            int version,
            string orderStatus,
            int orderVersion,
            bool canCompleteOrder,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var areaIds = await GetOrderAreaIdsAsync(orderId, cancellationToken);
                var payload = new
                {
                    orderId,
                    orderDetailId,
                    status,
                    version,
                    orderStatus,
                    orderVersion,
                    canCompleteOrder
                };

                await _hub.Clients.Groups(new[] { "kitchen", "staff" })
                    .SendAsync("OrderDetailStatusChanged", payload, cancellationToken);

                // Giữ sự kiện tổng quát để các màn hình bếp cũ chỉ cần tải lại Order.
                await _hub.Clients.Group("kitchen")
                    .SendAsync("OrderUpdated", new { orderId }, cancellationToken);

                foreach (var areaId in areaIds)
                {
                    await _hub.Clients.Group($"kitchen-area-{areaId}")
                        .SendAsync(
                            "OrderDetailStatusChanged",
                            new
                            {
                                orderId,
                                orderDetailId,
                                status,
                                version,
                                orderStatus,
                                orderVersion,
                                canCompleteOrder,
                                areaId
                            },
                            cancellationToken);

                    await _hub.Clients.Group($"kitchen-area-{areaId}")
                        .SendAsync("OrderUpdated", new { orderId, areaId }, cancellationToken);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "Không thể gửi OrderDetailStatusChanged cho món {OrderDetailId} của Order {OrderId}",
                    orderDetailId,
                    orderId);
                return false;
            }
        }

        public async Task<bool> NotifyOrderReadyAsync(int orderId, CancellationToken cancellationToken = default)
        {
            try
            {
                var areaIds = await GetOrderAreaIdsAsync(orderId, cancellationToken);

                await _hub.Clients.Groups(new[] { "kitchen", "staff" })
                    .SendAsync("OrderReady", new { orderId }, cancellationToken);

                foreach (var areaId in areaIds)
                {
                    await _hub.Clients.Group($"kitchen-area-{areaId}")
                        .SendAsync("OrderReady", new { orderId, areaId }, cancellationToken);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Không thể gửi OrderReady cho Order {OrderId}", orderId);
                return false;
            }
        }

        public async Task<bool> NotifyOrderCancelledAsync(int orderId, string reason, CancellationToken cancellationToken = default)
        {
            try
            {
                var areaIds = await GetOrderAreaIdsAsync(orderId, cancellationToken);

                await _hub.Clients.Groups(new[] { "kitchen", "staff" })
                    .SendAsync("OrderCancelled", new { orderId, reason }, cancellationToken);

                foreach (var areaId in areaIds)
                {
                    await _hub.Clients.Group($"kitchen-area-{areaId}")
                        .SendAsync("OrderCancelled", new { orderId, reason, areaId }, cancellationToken);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Không thể gửi OrderCancelled cho Order {OrderId}", orderId);
                return false;
            }
        }

        public async Task<bool> NotifyMenuAvailabilityChangedAsync(int menuItemId, bool isAvailable, CancellationToken cancellationToken = default)
        {
            try
            {
                var areaId = await _context.MenuItems.AsNoTracking()
                    .Where(x => x.MenuItemId == menuItemId)
                    .Select(x => x.KitchenAreaId)
                    .FirstOrDefaultAsync(cancellationToken);

                await _hub.Clients.All.SendAsync(
                    "MenuAvailabilityChanged",
                    new { menuItemId, isAvailable, areaId },
                    cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Không thể gửi MenuAvailabilityChanged cho món {MenuItemId}", menuItemId);
                return false;
            }
        }

        public async Task<bool> NotifyMenuItemChangedAsync(int menuItemId, string changeType, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await _context.MenuItems.AsNoTracking()
                    .Where(x => x.MenuItemId == menuItemId)
                    .Select(x => new
                    {
                        x.MenuItemId,
                        x.Name,
                        x.Category,
                        x.Price,
                        x.IsAvailable,
                        x.KitchenAreaId
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (item == null) return false;

                await _hub.Clients.All.SendAsync(
                    "MenuItemChanged",
                    new { changeType, item },
                    cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Không thể gửi MenuItemChanged cho món {MenuItemId}", menuItemId);
                return false;
            }
        }

        public async Task<bool> NotifyMenuItemDeletedAsync(int menuItemId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _hub.Clients.All.SendAsync(
                    "MenuItemDeleted",
                    new { menuItemId },
                    cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Không thể gửi MenuItemDeleted cho món {MenuItemId}", menuItemId);
                return false;
            }
        }

        public async Task<bool> NotifyPriorityChangedAsync(int orderId, int priority, CancellationToken cancellationToken = default)
        {
            try
            {
                var areaIds = await GetOrderAreaIdsAsync(orderId, cancellationToken);

                await _hub.Clients.Group("kitchen")
                    .SendAsync("PriorityChanged", new { orderId, priority }, cancellationToken);

                foreach (var areaId in areaIds)
                {
                    await _hub.Clients.Group($"kitchen-area-{areaId}")
                        .SendAsync("PriorityChanged", new { orderId, priority, areaId }, cancellationToken);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Không thể gửi PriorityChanged cho Order {OrderId}", orderId);
                return false;
            }
        }
    }
}
