using RestaurantManagement.Application.DTOs.Kitchen;

namespace RestaurantManagement.Application.Interfaces.Kitchen;

public interface IKitchenTrackingService
{
    Task<IReadOnlyList<MonAnBepDto>> LayDanhSachMonChoBepAsync(string? status);
    Task ChuyenSangDangCheBienAsync(int orderItemId, int userId, string role);
    Task HoanThanhMonAsync(int orderItemId, int userId, string role);
    Task HuyMonAsync(int orderItemId, int userId, string role);
    Task DanhDauChamMonAsync(int orderItemId, DanhDauChamMonRequest request, int userId, string role, bool isAutoDetected = false);
    Task HuyDanhDauChamMonAsync(int orderItemId, HuyDanhDauChamMonRequest request, int userId, string role);
    Task<AutoDetectDelayResult> TuDongPhatHienMonChamAsync(decimal thresholdMultiplier, int systemUserId);
}
