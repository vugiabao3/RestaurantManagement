namespace RestaurantManagement.Application.Interfaces;

public interface ILoyaltyService
{
    /// <summary>
    /// BR01: Tính số điểm nhận được dựa trên tổng hóa đơn (100.000 VNĐ = 1 điểm).
    /// </summary>
    int CalculateEarnedPoints(decimal totalAmount);

    /// <summary>
    /// BR02: Kiểm tra xem khách hàng có đủ điều kiện dùng điểm không (>= 10 điểm).
    /// </summary>
    bool CanUsePoints(int currentPoints, int pointsToUse);

    /// <summary>
    /// BR02: Tính số tiền được giảm giá dựa trên số điểm muốn dùng (1 điểm = 1.000 VNĐ).
    /// </summary>
    decimal CalculateDiscount(int pointsToUse);
}