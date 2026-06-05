namespace RestaurantManagement.Application.DTOs.Tracking;

public record MonAnTheoDoiDto(
    int OrderItemId,
    string TenMon,
    string? HinhAnh,
    int SoLuong,
    string TrangThai,
    DateTime ThoiGianDat);

public record TienDoMonAnDto(
    int TableId,
    int OrderId,
    int EstimatedWaitingTime,
    IReadOnlyList<MonAnTheoDoiDto> Items);

public record ThongBaoDto(
    int NotificationId,
    int OrderItemId,
    string NoiDung,
    DateTime ThoiGianTao);