using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs.Tracking;
using RestaurantManagement.Application.Interfaces.Tracking;
using RestaurantManagement.Domain.Constants.Tracking;
using RestaurantManagement.Domain.Entities.Tracking;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Services.Tracking;

public class OrderTrackingService : IOrderTrackingService
{
    private readonly AppDbContext _db;

    public OrderTrackingService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<TienDoMonAnDto> LayTienDoMonAnAsync(int tableId)
    {
        var order = await _db.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .FirstOrDefaultAsync(o => o.TableId == tableId && o.Status == "ACTIVE");

        if (order is null)
        {
            return new TienDoMonAnDto(tableId, 0, 0, Array.Empty<MonAnTheoDoiDto>());
        }

        var items = order.OrderItems
            .Where(i => i.ItemStatus != FoodStatusConstants.Cancelled)
            .OrderBy(i => i.OrderedAt)
            .ToList();

        var resultItems = items.Select(i => new MonAnTheoDoiDto(
            i.OrderItemId,
            i.MenuItem?.Name ?? string.Empty,
            i.MenuItem?.ImageUrl,
            i.Quantity,
            i.ItemStatus,
            i.OrderedAt
        )).ToList();

        return new TienDoMonAnDto(
            tableId,
            order.OrderId,
            TinhThoiGianChoUocTinh(items),
            resultItems
        );
    }

    private static int TinhThoiGianChoUocTinh(IEnumerable<OrderItem> items)
    {
        var now = DateTime.Now;

        var unfinished = items.Where(i =>
            i.ItemStatus == FoodStatusConstants.Pending ||
            i.ItemStatus == FoodStatusConstants.Cooking);

        return unfinished.Aggregate(0, (max, item) =>
        {
            var standard = item.MenuItem?.CookingTimeStandard ?? 0;

            var elapsed = item.ItemStatus == FoodStatusConstants.Cooking
                ? Math.Max(0, (int)Math.Floor((now - item.OrderedAt).TotalMinutes))
                : 0;

            var remaining = Math.Max(0, standard - elapsed);

            return Math.Max(max, remaining);
        });
    }

    public async Task<IReadOnlyList<ThongBaoDto>> LayThongBaoAsync(int tableId)
    {
        return await _db.Notifications
            .Where(n => n.TableId == tableId && !n.IsDisplayed)
            .OrderBy(n => n.CreateAt)
            .Select(n => new ThongBaoDto(
                n.NotificationId,
                n.OrderItemId,
                n.Content,
                n.CreateAt
            ))
            .ToListAsync();
    }

    public async Task DanhDauDaHienThiThongBaoAsync(int notificationId)
    {
        var notification = await _db.Notifications.FindAsync(notificationId)
            ?? throw new NghiepVuException("Không tìm thấy thông báo.", 404);

        notification.IsDisplayed = true;
        notification.DisplayedAt = DateTime.Now;
        notification.QueueStatus = "SENT";

        await _db.SaveChangesAsync();
    }
}