using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Infrastructure.Services;

public class LoyaltyService : ILoyaltyService
{
    // Cấu hình các hằng số theo Business Rules (BR)
    private const decimal AmountPerPoint = 100000m; // BR01: 100.000 VNĐ / 1 điểm
    private const decimal DiscountPerPoint = 1000m; // BR02: 1 điểm = 1.000 VNĐ
    private const int MinPointsToUse = 10;          // BR02: Tối thiểu 10 điểm mới được dùng

    public int CalculateEarnedPoints(decimal totalAmount)
    {
        // BR01: Lấy phần nguyên của phép chia Tổng tiền / 100.000
        if (totalAmount <= 0) 
        {
            return 0;
        }

        return (int)(totalAmount / AmountPerPoint);
    }

    public bool CanUsePoints(int currentPoints, int pointsToUse)
    {
        // BR02: Phải có ít nhất 10 điểm trong tài khoản.
        // Đồng thời số điểm muốn dùng phải hợp lệ (> 0 và không vượt quá số điểm đang có).
        if (currentPoints < MinPointsToUse)
        {
            return false;
        }

        return pointsToUse > 0 && pointsToUse <= currentPoints;
    }

    public decimal CalculateDiscount(int pointsToUse)
    {
        // BR02: 1 điểm = 1.000 VNĐ
        if (pointsToUse <= 0) 
        {
            return 0m;
        }

        return pointsToUse * DiscountPerPoint;
    }
}