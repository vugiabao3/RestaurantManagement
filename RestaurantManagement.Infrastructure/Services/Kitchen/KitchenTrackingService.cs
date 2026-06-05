using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs.Kitchen;
using RestaurantManagement.Application.Interfaces.Kitchen;
using RestaurantManagement.Domain.Constants.Tracking;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Services.Kitchen;

public class KitchenTrackingService : IKitchenTrackingService
{
    private readonly AppDbContext _db;

    public KitchenTrackingService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<MonAnBepDto>> LayDanhSachMonChoBepAsync(string? status)
    {
        var query = _db.OrderItems
            .Include(i => i.MenuItem)
            .Include(i => i.Order).ThenInclude(o => o!.Table)
            .Include(i => i.DelayLogs)
            .Where(i => i.Order!.Status == "ACTIVE");

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(i => i.ItemStatus == status);
        }

        return await query
            .OrderBy(i => i.OrderedAt)
            .Select(i => new MonAnBepDto(
                i.OrderItemId,
                i.OrderId,
                i.Order!.TableId,
                i.Order.Table!.TableNumber,
                i.MenuItem!.Name,
                i.Quantity,
                i.ItemStatus,
                i.IsDelayed,
                i.IsDelayed ? (int?)EF.Functions.DateDiffMinute(i.OrderedAt, DateTime.Now) : null,
                i.DelayLogs
                    .Where(l => l.Status == "ACTIVE")
                    .OrderByDescending(l => l.StartedAt)
                    .Select(l => l.DelayReason)
                    .FirstOrDefault(),
                i.OrderedAt))
            .ToListAsync();
    }

    public async Task ChuyenSangDangCheBienAsync(int orderItemId, int userId, string role)
    {
        KiemTraQuyenBep(role);

        var item = await LayOrderItemAsync(orderItemId);
        KiemTraChuyenTrangThai(item.ItemStatus, FoodStatusConstants.Cooking);

        item.ItemStatus = FoodStatusConstants.Cooking;
        await _db.SaveChangesAsync();
    }

    public async Task HoanThanhMonAsync(int orderItemId, int userId, string role)
    {
        KiemTraQuyenBep(role);

        await using var tx = await _db.Database.BeginTransactionAsync();
        try
        {
            var item = await LayOrderItemAsync(orderItemId);
            KiemTraChuyenTrangThai(item.ItemStatus, FoodStatusConstants.Done);

            item.ItemStatus = FoodStatusConstants.Done;
            item.CompletedAt = DateTime.Now;
            item.IsDelayed = false;

            var activeDelayLogs = await _db.KitchenDelayLogs
                .Where(l => l.OrderItemId == orderItemId && l.Status == "ACTIVE")
                .ToListAsync();

            foreach (var log in activeDelayLogs)
            {
                log.Status = "RESOLVED";
                log.ResolvedAt = DateTime.Now;
                log.DelayDuration = Math.Max(1, (int)Math.Ceiling((DateTime.Now - log.StartedAt).TotalMinutes));
            }

            var daCoThongBao = await _db.Notifications.AnyAsync(n => n.OrderItemId == orderItemId);
            if (!daCoThongBao)
            {
                var content = $"{item.MenuItem?.Name ?? "Món ăn"} của bạn đã sẵn sàng! Vui lòng di chuyển đến Quầy Trả Món để nhận đồ ăn. Chúc quý khách ngon miệng!";
                _db.Notifications.Add(new Notification
                {
                    OrderItemId = item.OrderItemId,
                    TableId = item.Order!.TableId,
                    Content = content,
                    IsDisplayed = false,
                    QueueStatus = "PENDING",
                    CreateAt = DateTime.Now
                });
            }

            await _db.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    public async Task HuyMonAsync(int orderItemId, int userId, string role)
    {
        KiemTraQuyenBep(role);

        var item = await LayOrderItemAsync(orderItemId);
        KiemTraChuyenTrangThai(item.ItemStatus, FoodStatusConstants.Cancelled);

        item.ItemStatus = FoodStatusConstants.Cancelled;
        item.IsDelayed = false;

        var activeDelayLogs = await _db.KitchenDelayLogs
            .Where(l => l.OrderItemId == orderItemId && l.Status == "ACTIVE")
            .ToListAsync();

        foreach (var log in activeDelayLogs)
        {
            log.Status = "CANCELLED";
            log.ResolvedAt = DateTime.Now;
        }

        await _db.SaveChangesAsync();
    }

    public async Task DanhDauChamMonAsync(int orderItemId, DanhDauChamMonRequest request, int userId, string role, bool isAutoDetected = false)
    {
        if (!isAutoDetected)
        {
            KiemTraQuyenBep(role);
        }

        var item = await LayOrderItemAsync(orderItemId);

        // Chỉ đánh dấu chậm khi món đang COOKING.
        // Món PENDING chưa bắt đầu chế biến nên chưa xem là chậm do quá trình nấu.
        if (item.ItemStatus != FoodStatusConstants.Cooking)
        {
            throw new NghiepVuException("Chỉ có thể đánh dấu chậm cho món đang chế biến (COOKING).");
        }

        if (await _db.KitchenDelayLogs.AnyAsync(l => l.OrderItemId == orderItemId && l.Status == "ACTIVE"))
        {
            throw new NghiepVuException("Món này đã có bản ghi chậm đang hoạt động.");
        }

        var delayReason = DelayReasonConstants.Normalize(request.ReasonCode);
        if (delayReason == DelayReasonConstants.Khac && (request.DelayNotes?.Trim().Length ?? 0) < 10)
        {
            throw new NghiepVuException("Khi chọn lý do Khác, ghi chú phải có tối thiểu 10 ký tự.");
        }

        item.IsDelayed = true;
        _db.KitchenDelayLogs.Add(new KitchenDelayLog
        {
            OrderItemId = orderItemId,
            ChefUserId = userId,
            DelayReason = delayReason,
            DelayPriority = request.DelayPriority,
            DelayNotes = request.DelayNotes,
            IsAutoDetected = isAutoDetected,
            Status = "ACTIVE",
            StartedAt = DateTime.Now
        });

        await _db.SaveChangesAsync();
    }

    public async Task HuyDanhDauChamMonAsync(int orderItemId, HuyDanhDauChamMonRequest request, int userId, string role)
    {
        KiemTraQuyenBep(role);

        var item = await LayOrderItemAsync(orderItemId);
        var activeLogs = await _db.KitchenDelayLogs
            .Where(l => l.OrderItemId == orderItemId && l.Status == "ACTIVE")
            .ToListAsync();

        if (!activeLogs.Any())
        {
            throw new NghiepVuException("Món này không có trạng thái chậm đang hoạt động.", 404);
        }

        item.IsDelayed = false;
        foreach (var log in activeLogs)
        {
            log.Status = "CANCELLED";
            log.ResolvedAt = DateTime.Now;
            log.DelayNotes = string.IsNullOrWhiteSpace(request.LyDoHuy)
                ? log.DelayNotes
                : $"{log.DelayNotes}; Hủy: {request.LyDoHuy}";
        }

        await _db.SaveChangesAsync();
    }

    public async Task<AutoDetectDelayResult> TuDongPhatHienMonChamAsync(decimal thresholdMultiplier, int systemUserId)
    {
        if (thresholdMultiplier <= 0)
        {
            thresholdMultiplier = 1.5m;
        }

        var now = DateTime.Now;
        var candidates = await _db.OrderItems
            .Include(i => i.MenuItem)
            .Include(i => i.DelayLogs)
            .Where(i => i.ItemStatus == FoodStatusConstants.Cooking && !i.IsDelayed && i.MenuItem != null)
            .ToListAsync();

        var marked = new List<int>();
        foreach (var item in candidates)
        {
            var elapsed = (decimal)(now - item.OrderedAt).TotalMinutes;
            var threshold = item.MenuItem!.CookingTimeStandard * thresholdMultiplier;

            if (elapsed <= threshold)
            {
                continue;
            }

            item.IsDelayed = true;
            _db.KitchenDelayLogs.Add(new KitchenDelayLog
            {
                OrderItemId = item.OrderItemId,
                ChefUserId = systemUserId,
                DelayReason = DelayReasonConstants.DonHangDon,
                DelayPriority = "MEDIUM",
                DelayNotes = "Hệ thống tự động phát hiện món vượt ngưỡng thời gian chuẩn.",
                IsAutoDetected = true,
                Status = "ACTIVE",
                StartedAt = now
            });
            marked.Add(item.OrderItemId);
        }

        await _db.SaveChangesAsync();
        return new AutoDetectDelayResult(marked.Count, marked);
    }

    private async Task<OrderItem> LayOrderItemAsync(int orderItemId)
    {
        return await _db.OrderItems
            .Include(i => i.MenuItem)
            .Include(i => i.Order)
            .FirstOrDefaultAsync(i => i.OrderItemId == orderItemId)
            ?? throw new NghiepVuException("Không tìm thấy món trong đơn hàng.", 404);
    }

    private static void KiemTraChuyenTrangThai(string current, string next)
    {
        if (!FoodStatusConstants.CanChange(current, next))
        {
            throw new NghiepVuException($"Không thể chuyển trạng thái từ {current} sang {next}.");
        }
    }

    private static void KiemTraQuyenBep(string role)
    {
        if (role is not (UserRoleConstants.DauBep or UserRoleConstants.QuanLy or UserRoleConstants.Admin))
        {
            throw new NghiepVuException("Bạn không có quyền thực hiện thao tác này.", 403);
        }
    }
}
