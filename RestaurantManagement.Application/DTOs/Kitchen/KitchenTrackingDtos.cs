namespace RestaurantManagement.Application.DTOs.Kitchen;

public record MonAnBepDto(
    int OrderItemId,
    int OrderId,
    int TableId,
    string MaBan,
    string TenMon,
    int SoLuong,
    string TrangThai,
    bool IsDelayed,
    int? SoPhutCham,
    string? LyDoCham,
    DateTime ThoiGianDat);

public record DanhDauChamMonRequest(
    string ReasonCode,
    string DelayPriority,
    string? DelayNotes);

public record HuyDanhDauChamMonRequest(string? LyDoHuy);

public record AutoDetectDelayRequest(decimal? ThresholdMultiplier);

public record AutoDetectDelayResult(int SoMonDuocPhatHien, IReadOnlyList<int> OrderItemIds);
