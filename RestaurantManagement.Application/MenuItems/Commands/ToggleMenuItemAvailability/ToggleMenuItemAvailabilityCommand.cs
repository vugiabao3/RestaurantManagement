using MediatR;

namespace RestaurantManagement.Application.MenuItems.Commands.ToggleMenuItemAvailability
{
    /// <summary>
    /// UC_KIT_03 - Alt 5c: Bấm nút chuyển trạng thái Còn/Hết món
    /// ngay trên bảng danh sách (không qua Modal).
    /// </summary>
    public class ToggleMenuItemAvailabilityCommand
        : IRequest<ToggleMenuItemAvailabilityResponse>
    {
        public int MenuItemId { get; set; }

        public bool IsAvailable { get; set; }
    }
}
