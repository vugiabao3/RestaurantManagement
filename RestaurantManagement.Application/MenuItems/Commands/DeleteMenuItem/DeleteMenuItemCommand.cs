using MediatR;

namespace RestaurantManagement.Application.MenuItems.Commands.DeleteMenuItem
{
    /// <summary>
    /// UC_KIT_03 - Alt 5b/7b: Xóa món ăn (sau khi đã xác nhận ở FE).
    /// </summary>
    public class DeleteMenuItemCommand
        : IRequest<DeleteMenuItemResponse>
    {
        public int MenuItemId { get; set; }
    }
}
