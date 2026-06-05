using RestaurantManagement.Application.DTOs.Tracking;

namespace RestaurantManagement.Application.Interfaces.Tracking;

public interface IOrderTrackingService
{
    Task<TienDoMonAnDto> LayTienDoMonAnAsync(int tableId);

    Task<IReadOnlyList<ThongBaoDto>> LayThongBaoAsync(int tableId);

    Task DanhDauDaHienThiThongBaoAsync(int notificationId);
}